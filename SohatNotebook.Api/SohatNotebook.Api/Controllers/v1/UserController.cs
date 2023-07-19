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
    public class UserController : BaseController
    {
        public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager) : base(unitOfWork, userManager)
        {
        }

        [Route("getall")]
        [HttpGet()]
        public IActionResult GetAll()
        {
            Task<IEnumerable<UserDb>>? userDbs = _unitOfWork.Users.GetAll();
            return Ok(userDbs);
        }

        [Route("getbyid")]
        [HttpGet()]
        public async Task<IActionResult> GetById(Guid userId)
        {
            UserDb? userDb = await _unitOfWork.Users.GetById(userId);
            return Ok(userDb);
        }

        [Route("getbyemail")]
        [HttpGet()]
        public async Task<IActionResult> GetByEmail(string email)
        {
            UserDb? userDb = await _unitOfWork.Users.GetByEmail(email);
            return Ok(userDb);
        }

        [Route("adduser")]
        [HttpPost()]
        public IActionResult Add(UserDto userDto)
        {
            UserDb userDb = Map(userDto);
            _unitOfWork.Users.Add(userDb);
            _unitOfWork.CompleteAsync();
            Guid userId = _unitOfWork.Users.GetByEmail(userDto.Email).GetAwaiter().GetResult().Id;
            return CreatedAtRoute("getbyid", userId);
        }

        private static UserDb Map(UserDto userDto)
        {
            return new UserDb()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                DateOfBirth = userDto.DateOfBirth,
                Country = userDto.Country,
                Profession = userDto.Profession,
                Hobby = userDto.Hobby,
                Status = 1
            };
        }
    }
}