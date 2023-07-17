using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SohatNotebook.Authentication.Configuration;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AccountController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> options) 
            : base(unitOfWork)
        {
            _userManager = userManager;
            _jwtConfig = options.CurrentValue;
        }

        [Route("register")]
        [HttpPost()]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto userRegistrationRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(new UserRegistrationResponse()
            {
                Success = false,
                Errors = new List<string>() { "Invalid payload" }
            });

            IdentityUser? user = await _userManager.FindByEmailAsync(userRegistrationRequestDto.Email);
            if (user != null) return BadRequest(new UserRegistrationResponse()
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
            if (!identityResult.Succeeded) return BadRequest(new UserRegistrationResponse()
            {
                Success = false,
                Errors = identityResult.Errors.Select(e => e.Description).ToList()
            });

            UserDb? userDb = Map(userRegistrationRequestDto, newUser.Id);
            await _unitOfWork.Users.Add(userDb);
            await _unitOfWork.CompleteAsync();

            var token = GenerateJwtToken(newUser);

            return Ok(new UserRegistrationResponse()
            {
                Success = true,
                Token = token,
            });
        }

        [Route("login")]
        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userLoginRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(new UserRegistrationResponse()
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

            var token = GenerateJwtToken(user);

            return Ok(new UserLoginResponse()
            {
                Success = true,
                Token = token,
            });
        }

        private string GenerateJwtToken(IdentityUser identityUser)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            byte[]? key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = GetClaims(identityUser),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken? token = jwtHandler.CreateToken(tokenDescriptor);
            string? jwtToken = jwtHandler.WriteToken(token);

            return jwtToken;
        }

        private static ClaimsIdentity GetClaims(IdentityUser identityUser)
        {
            return new ClaimsIdentity( new[]
            {
                new Claim("Id", identityUser.Id),
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
    }
}
