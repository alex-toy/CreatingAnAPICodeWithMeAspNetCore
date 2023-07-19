using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SohatNotebook.DataService.Configuration;
using SohatNotebook.Entities.Dto.Generics;

namespace SohatNotebook.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BaseController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        protected readonly UserManager<IdentityUser> _userManager;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        protected Error SetError(int code, string message, string type)
        {
            return new Error() { Code = code, Message = message, Type = type };
        }
    }
}
