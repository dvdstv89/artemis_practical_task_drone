using drones.API.Services;
using drones.API.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace drones.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : BaseController<LogController>
    {
        private readonly IPeriodicTaskLogService _periodicTaskLogService;
        public LogController(IPeriodicTaskLogService periodicTaskLogService, ILogger<LogController> logger) : base(logger)
        {
            _periodicTaskLogService = periodicTaskLogService;
        }

        [HttpPost("{serialNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = MessageText.ENDPOINT_NAME_SHOW_LOGS, Tags = new[] { "Logs" })]
        public async Task<ActionResult<ApiResponse>> GetDroneBatteryLog(string serialNumber)
        {
            ApiResponse response = await _periodicTaskLogService.GetAllsByIdAsync(serialNumber);
            return await HandleApiResponse(response, MessageText.ENDPOINT_NAME_SHOW_LOGS);
        }
    }
}
