using drones.API.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace drones.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : BaseController<DroneController>
    {
        public DroneController(ILogger<DroneController> logger) : base(logger)
        {

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Registering a new drone")]
        public async Task<ActionResult<Drone>> RegisterNewDrone([FromBody] Drone drone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(drone);
        }
    }
}
