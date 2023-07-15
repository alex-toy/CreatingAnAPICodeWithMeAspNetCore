using Microsoft.AspNetCore.Mvc;
using SohatNotebook.DataService.Data;
using SohatNotebook.Entities.DbSet;
using SohatNotebook.Entities.Dto.Incoming;

namespace SohatNotebook.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _appDbContext;

        public UserController(ILogger<UserController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [Route("getall")]
        [HttpGet()]
        public IActionResult GetAll()
        {
            List<UserDb>? userDbs = _appDbContext.Users.Where(u => u.Status == 1).ToList();
            return Ok(userDbs);
        }

        [Route("getbyid")]
        [HttpGet()]
        public IActionResult GetById(Guid userId)
        {
            UserDb? userDb = _appDbContext.Users.FirstOrDefault(u => u.Id == userId);
            return Ok(userDb);
        }

        [Route("add")]
        [HttpPost()]
        public IActionResult Add(UserDto userDto)
        {
            UserDb user = Map(userDto);
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return Ok();
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