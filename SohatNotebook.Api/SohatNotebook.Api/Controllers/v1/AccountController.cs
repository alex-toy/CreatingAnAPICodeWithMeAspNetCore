using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SohatNotebook.Authentication.Configuration;
using SohatNotebook.Authentication.Models.Incoming;
using SohatNotebook.Authentication.Models.Outcoming;
using SohatNotebook.DataService.Configuration;
using System.IdentityModel.Tokens.Jwt;
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
        }

        private string GenerateJwtToken(IdentityUser identityUser)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            byte[]? key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {

            };
        }
    }
}
