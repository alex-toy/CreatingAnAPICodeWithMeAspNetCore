using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SohatNotebook.DataService.Configuration;
using SohatNotebook.Entities.DbSet;
using SohatNotebook.Entities.Dto.Incoming;

namespace SohatNotebook.Api.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController : BaseController
    {
        public ProfileController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager) : base(unitOfWork, userManager)
        {
        }

        [Route("getprofile")]
        [HttpGet()]
        public async Task<IActionResult> GetProfile()
        {
            IdentityUser? loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

            if (loggedInUser == null) return BadRequest("user not found");

            var identityId = new Guid(loggedInUser.Id);

            UserDb profile = await _unitOfWork.Users.GetByIdentityId(identityId);

            if (profile == null) return BadRequest("user not found");

            return Ok(profile);
        }

        [Route("updateprofile")]
        [HttpPut()]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfileDto)
        {
            if (!ModelState.IsValid) return BadRequest("invalid payload");

            IdentityUser? loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

            if (loggedInUser == null) return BadRequest("user not found");

            var identityId = new Guid(loggedInUser.Id);

            UserDb newProfile = await _unitOfWork.Users.GetByIdentityId(identityId);

            newProfile.Country = updateProfileDto.Country;
            newProfile.Address = updateProfileDto.Address;
            newProfile.MobileNumber = updateProfileDto.MobileNumber;
            newProfile.Gender = updateProfileDto.Gender;

            bool isUpdated = await _unitOfWork.Users.Update(newProfile);

            if (!isUpdated) return BadRequest("error while updating");

            await _unitOfWork.CompleteAsync();
            return Ok(newProfile);
        }
    }
}