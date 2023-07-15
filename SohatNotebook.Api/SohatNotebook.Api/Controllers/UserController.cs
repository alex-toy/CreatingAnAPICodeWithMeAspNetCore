using Microsoft.AspNetCore.Mvc;
using SohatNotebook.DataService.Data;
using SohatNotebook.Entities.DbSet;

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

        [HttpGet(Name = "test")]
        public IActionResult GetAll()
        {
            List<User>? users = _appDbContext.Users.Where(u => u.Status == 1).ToList();
            return Ok(users);
        }
    }
}