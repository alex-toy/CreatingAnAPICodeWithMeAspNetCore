using Microsoft.AspNetCore.Mvc;
using SohatNotebook.DataService.Configuration;

namespace SohatNotebook.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BaseController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
