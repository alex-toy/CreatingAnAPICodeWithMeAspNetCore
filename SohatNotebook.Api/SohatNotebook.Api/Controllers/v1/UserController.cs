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
    public class UserController : BaseController
    {
        public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager) : base(unitOfWork, userManager)
        {
        }

        [Route("getall")]
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<UserDb>? userDbs = await _unitOfWork.Users.GetAll();
            var result = new PagedResult<UserDb>() { Content = userDbs.ToList(), ResultCount = userDbs.Count() };
            return Ok(result);
        }

        [Route("getbyid")]
        [HttpGet()]
        public async Task<IActionResult> GetById(Guid userId)
        {
            UserDb? userDb = await _unitOfWork.Users.GetById(userId);

            if (userDb == null)
            {
                var result = new Result<UserDb>() { Error = SetError(400, ErrorMessages.UserNotFound, ErrorMessages.BadRequest) };
                return Ok(result);
            }

            var okResult = new Result<UserDb>() { Content = userDb };
            return Ok(okResult);
        }

        [Route("getbyemail")]
        [HttpGet()]
        public async Task<IActionResult> GetByEmail(string email)
        {
            UserDb? userDb = await _unitOfWork.Users.GetByEmail(email);

            if (userDb == null)
            {
                var result = new Result<UserDb>() { Error = SetError(400, ErrorMessages.UserNotFound, ErrorMessages.BadRequest) };
                return Ok(result);
            }

            var okResult = new Result<UserDb>() { Content = userDb };
            return BadRequest(okResult);
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