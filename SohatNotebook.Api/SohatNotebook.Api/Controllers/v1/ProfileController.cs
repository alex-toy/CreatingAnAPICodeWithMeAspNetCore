using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SohatNotebook.DataService.Configuration;
using SohatNotebook.Entities.DbSet;
using SohatNotebook.Entities.Dto.Generics;
using SohatNotebook.Entities.Dto.Incoming;

namespace SohatNotebook.Api.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController : BaseController
    {
        public ProfileController(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 UserManager<IdentityUser> userManager) : base(unitOfWork, userManager, mapper)
        {
        }

        [Route("getprofile")]
        [HttpGet()]
        public async Task<IActionResult> GetProfile()
        {
            IdentityUser? loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

            if (loggedInUser == null)
            {
                var result = new Result<UserDb>() { Error = SetError(400, ErrorMessages.UserNotFound, ErrorMessages.BadRequest) };
                return BadRequest(result);
            }

            var identityId = new Guid(loggedInUser.Id);

            UserDb profile = await _unitOfWork.Users.GetByIdentityId(identityId);

            if (profile == null)
            {
                var result = new Result<UserDb>() { Error = SetError(400, ErrorMessages.ProfileNotFound, ErrorMessages.BadRequest) };
                return BadRequest(result);
            }

            var okResult = new Result<UserDto>() { Content = _mapper.Map<UserDto>(profile) };
            return Ok(okResult);
        }

        [Route("updateprofile")]
        [HttpPut()]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfileDto)
        {
            if (!ModelState.IsValid) return BadRequest("invalid payload");

            IdentityUser? loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

            if (loggedInUser == null)
            {
                var result = new Result<UserDb>() { Error = SetError(400, ErrorMessages.UserNotFound, ErrorMessages.BadRequest) };
                return BadRequest(result);
            }

            var identityId = new Guid(loggedInUser.Id);

            UserDb newProfile = await _unitOfWork.Users.GetByIdentityId(identityId);


            newProfile.Country = updateProfileDto.Country;
            newProfile.Address = updateProfileDto.Address;
            newProfile.MobileNumber = updateProfileDto.MobileNumber;
            newProfile.Gender = updateProfileDto.Gender;

            bool isUpdated = await _unitOfWork.Users.Update(newProfile);

            if (!isUpdated)
            {
                var result = new Result<UserDb>() { Error = SetError(500, ErrorMessages.ErrorWhileUpdating, ErrorMessages.BadRequest)  };
                return BadRequest(result);
            }

            await _unitOfWork.CompleteAsync();
            UserDto? temp = _mapper.Map<UserDto>(updateProfileDto);
            var okResult = new Result<UserDto>() { Content = temp, Error = null };
            return Ok(okResult);
        }
    }
}