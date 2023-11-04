using drones.API.Models;
using Microsoft.AspNetCore.Mvc;

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
