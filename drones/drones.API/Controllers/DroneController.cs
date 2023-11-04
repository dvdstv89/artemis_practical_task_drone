using drones.API.Models;
using drones.API.Services;
using drones.API.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace drones.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : BaseController<DroneController>
    {
        private readonly IDroneService _service;
        public DroneController(ILogger<DroneController> logger, IDroneService droneService) : base(logger)
        {
            _service = droneService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Registering a new drone")]
        public async Task<ActionResult<ApiResponse>> RegisterNewDrone([FromBody] Drone drone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApiResponse response = await _service.RegisterDroneAsync(drone);
            return await ProcessResponse(response);
        }
    }
}
