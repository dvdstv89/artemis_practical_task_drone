using Microsoft.AspNetCore.Mvc;

namespace drones.API.Controllers
{
    public class BaseController<T> : Controller
    {
        protected readonly ILogger<T> _logger;
        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
