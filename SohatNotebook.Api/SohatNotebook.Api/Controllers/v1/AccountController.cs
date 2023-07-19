using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SohatNotebook.Authentication.Configuration;
using SohatNotebook.Authentication.Models.Generic;
using SohatNotebook.Authentication.Models.Incoming;
using SohatNotebook.Authentication.Models.Outcoming;
using SohatNotebook.DataService.Configuration;
using SohatNotebook.Entities.DbSet;
using SohatNotebook.Entities.Dto.Incoming;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SohatNotebook.Api.Controllers.v1
{
    public class AccountController : BaseController
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtConfig _jwtConfig;

        public AccountController(IUnitOfWork unitOfWork,
                                 UserManager<IdentityUser> userManager,
                                 IOptionsMonitor<JwtConfig> options,
                                 TokenValidationParameters tokenValidationParameters)
            : base(unitOfWork, userManager)
        {
            _jwtConfig = options.CurrentValue;
            _tokenValidationParameters = tokenValidationParameters;
        }

        [Route("register")]
        [HttpPost()]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto userRegistrationRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(new UserRegistrationResponseDto()
            {
                Success = false,
                Errors = new List<string>() { "Invalid payload" }
            });

            IdentityUser? user = await _userManager.FindByEmailAsync(userRegistrationRequestDto.Email);
            if (user != null) return BadRequest(new UserRegistrationResponseDto()
            {
                Success = false,
                Errors = new List<string>() { "email already in use" }
            });

            var newUser = new IdentityUser()
            {
                Email = userRegistrationRequestDto.Email,
                UserName = userRegistrationRequestDto.Email,
                EmailConfirmed = true,
            };

            IdentityResult? identityResult = await _userManager.CreateAsync(newUser, userRegistrationRequestDto.Password);
            if (!identityResult.Succeeded) return BadRequest(new UserRegistrationResponseDto()
            {
                Success = false,
                Errors = identityResult.Errors.Select(e => e.Description).ToList()
            });

            UserDb? userDb = Map(userRegistrationRequestDto, newUser.Id);
            await _unitOfWork.Users.Add(userDb);
            await _unitOfWork.CompleteAsync();

            var token = await GenerateJwtToken(newUser);

            return Ok(new UserRegistrationResponseDto()
            {
                Success = true,
                Token = token.JwtToken,
                RefreshToken = token.RefreshToken,
            });
        }

        [Route("login")]
        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userLoginRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(new UserRegistrationResponseDto()
            {
                Success = false,
                Errors = new List<string>() { "Invalid payload" }
            });

            IdentityUser? user = await _userManager.FindByEmailAsync(userLoginRequestDto.Email);
            if (user == null) return BadRequest(new UserLoginResponse()
            {
                Success = false,
                Errors = new List<string>() { "user doesn't exist" }
            });

            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, userLoginRequestDto.Password);
            if (!isPasswordCorrect) return BadRequest(new UserLoginResponse()
            {
                Success = false,
                Errors = new List<string>() { "credentials not recognized" }
            });

            var token = await GenerateJwtToken(user);

            return Ok(new UserLoginResponse()
            {
                Success = true,
                Token = token.JwtToken,
                RefreshToken = token.RefreshToken,
            });
        }

        [Route("refreshtoken")]
        [HttpPost()]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto tokenRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(new UserRegistrationResponseDto()
            {
                Success = false,
                Errors = new List<string>() { "Invalid payload" }
            });

            AuthResult authResult = await VerifyToken(tokenRequestDto);
            if (authResult == null) return BadRequest(new UserRegistrationResponseDto()
            {
                Success = false,
                Errors = new List<string>() { "token validation failed" }
            });

            return Ok(authResult);
        }

        private async Task<AuthResult> VerifyToken(TokenRequestDto tokenRequestDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal? principal = tokenHandler.ValidateToken(tokenRequestDto.Token, _tokenValidationParameters, out SecurityToken? validatedToken);
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    const string hmacSha256 = SecurityAlgorithms.HmacSha256;
                    StringComparison ignoreCase = StringComparison.InvariantCultureIgnoreCase;
                    bool createdWithHmacSha256 = jwtSecurityToken.Header.Alg.Equals(hmacSha256, ignoreCase);
                    if (!createdWithHmacSha256) return null;
                }

                string utcExpiryString = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value;
                long utcExpiryLong = long.Parse(utcExpiryString);
                DateTime expiryUtc = UnixTimeStampToDateTime(utcExpiryLong);
                if (expiryUtc > DateTime.UtcNow) return new AuthResult
                {
                    Success = false,
                    Errors = new List<string>() { "jwt token has not expired" }
                };

                RefreshTokenDb? refreshToken = await _unitOfWork.RefreshTokens.GetByRefreshToken(tokenRequestDto.RefreshToken);

                if (refreshToken is null) return new AuthResult
                {
                    Success = false,
                    Errors = new List<string>() { "invalid refresh token" }
                };

                if (refreshToken.ExpiryDate < DateTime.UtcNow) return new AuthResult
                {
                    Success = false,
                    Errors = new List<string>() { "refresh token has expired, please login again" }
                };

                if (refreshToken.IsUsed) return new AuthResult
                {
                    Success = false,
                    Errors = new List<string>() { "refresh token is already in use" }
                };

                if (refreshToken.IsRevoked) return new AuthResult
                {
                    Success = false,
                    Errors = new List<string>() { "refresh token is revoked" }
                };

                string? jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (refreshToken.JwtId != jti) return new AuthResult
                {
                    Success = false,
                    Errors = new List<string>() { "refresh token reference does not match the jwt token" }
                };

                refreshToken.IsUsed = true;
                bool markedAsUsed = await _unitOfWork.RefreshTokens.MarkRefreshTokenAsUSed(refreshToken);
                if (!markedAsUsed) return new AuthResult
                {
                    Success = false,
                    Errors = new List<string>() { "error processing request" }
                };

                await _unitOfWork.CompleteAsync();
                IdentityUser? userDb = await _userManager.FindByIdAsync(refreshToken.UserId);
                if (userDb == null) return new AuthResult
                {
                    Success = false,
                    Errors = new List<string>() { "error processing request" }
                };

                TokenData? token = await GenerateJwtToken(userDb);
                return new AuthResult
                {
                    Token = token.JwtToken,
                    RefreshToken = token.RefreshToken,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = new List<string>() { ex.Message }
                };
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixDate)
        {
            var datetime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            datetime = datetime.AddSeconds(unixDate).ToUniversalTime();
            return datetime;
        }

        private async Task<TokenData> GenerateJwtToken(IdentityUser identityUser)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            byte[]? key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = GetClaims(identityUser),
                Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken? token = jwtHandler.CreateToken(tokenDescriptor);
            string? jwtToken = jwtHandler.WriteToken(token);

            RefreshTokenDb refreshTokenDb = await GenerateRefreshToken(identityUser, token);

            var tokenData = new TokenData
            {
                JwtToken = jwtToken,
                RefreshToken = refreshTokenDb.Token,
            };

            return tokenData;
        }

        private async Task<RefreshTokenDb> GenerateRefreshToken(IdentityUser identityUser, SecurityToken token)
        {
            var refreshToken = new RefreshTokenDb
            {
                AddedData = DateTime.UtcNow,
                Token = GenerateRandomString(32),
                UserId = identityUser.Id,
                IsRevoked = false,
                IsUsed = false,
                Status = 1,
                JwtId = token.Id,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
            };

            await _unitOfWork.RefreshTokens.Add(refreshToken);
            await _unitOfWork.CompleteAsync();

            return refreshToken;
        }

        private static ClaimsIdentity GetClaims(IdentityUser identityUser)
        {
            return new ClaimsIdentity( new[]
            {
                new Claim("Id", identityUser.Id),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            });
        }

        private static UserDb Map(UserRegistrationRequestDto userDto, string id)
        {
            return new UserDb()
            {
                IdentityId = new Guid(id),
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Phone = String.Empty,
                DateOfBirth = DateTime.UtcNow,
                Country = String.Empty,
                Profession = String.Empty,
                Hobby = String.Empty,
                Status = 1
            };
        }

        private string GenerateRandomString(int length)
        {
            string randomString = "";

            for (int i = 0; i < length; i++)
            {
                Random random = new Random();
                var temp = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
                randomString += temp;
            }

            return randomString;
        }
    }
}
