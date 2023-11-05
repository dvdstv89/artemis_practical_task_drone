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

        protected async Task<ActionResult<ApiResponse>> HandleApiResponse(ApiResponse response, string endpoint)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (_logger != null) _logger.LogInformation(string.Format(MessageText.HANDLE_API_RESPONSE_OK, endpoint));
                return Ok(response);
            }
            if (response.StatusCode == HttpStatusCode.Created)
            {
                if (_logger != null) _logger.LogInformation(string.Format(MessageText.HANDLE_API_RESPONSE_CREATED, endpoint));
                return CreatedAtAction(null, null, response);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                if (_logger != null) _logger.LogInformation(string.Format(MessageText.HANDLE_API_RESPONSE_NOT_FOUND, endpoint));
                return NotFound(response);
            }
            if (_logger != null) _logger.LogError(string.Format(MessageText.HANDLE_API_RESPONSE_BAD_RESPONSE, endpoint));
            return BadRequest(response);
        }
    }
}
