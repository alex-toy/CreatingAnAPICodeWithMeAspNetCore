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
    public class UserController : BaseController
    {
        public UserController(IUnitOfWork unitOfWork,
                              UserManager<IdentityUser> userManager,
                              IMapper mapper) 
            : base(unitOfWork, userManager, mapper)
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

            var okResult = new Result<UserDto>() { Content = _mapper.Map<UserDto>(userDb) };
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
                return BadRequest(result);
            }

            var okResult = new Result<UserDto>() { Content = _mapper.Map<UserDto>(userDb) };
            return Ok(okResult);
        }

        [Route("adduser")]
        [HttpPost()]
        public IActionResult Add(UserDto userDto)
        {
            UserDb? userDb = _mapper.Map<UserDb>(userDto);
            _unitOfWork.Users.Add(userDb);
            _unitOfWork.CompleteAsync();

            var result = new Result<UserDto>() { Content = userDto }; 
            Guid userId = _unitOfWork.Users.GetByEmail(userDto.Email).GetAwaiter().GetResult().Id;
            return CreatedAtAction("GetById", new { id = userId }, result);
        }
    }
}