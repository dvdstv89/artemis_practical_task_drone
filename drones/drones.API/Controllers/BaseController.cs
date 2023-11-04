using drones.API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace drones.API.Controllers
{
    public class BaseController<T> : Controller
    {
        protected readonly ILogger<T> _logger;
        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected async Task<ActionResult<ApiResponse>> ProcessResponse(ApiResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation("");
                return Ok(response);
            }
            if (response.StatusCode == HttpStatusCode.Created)
            {
                _logger.LogInformation("");
                return Ok(response);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogInformation("");
                return NotFound(response);
            }
            _logger.LogError("");
            return BadRequest(response);
        }
    }
}
