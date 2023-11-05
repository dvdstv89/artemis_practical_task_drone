using drones.API.DTO;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = MessageText.ENDPOINT_NAME_REGISTER_DRONE)]
        public async Task<ActionResult<ApiResponse>> RegisterNewDrone([FromBody] Drone drone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApiResponse response = await _service.RegisterDroneAsync(drone);
            return await HandleApiResponse(response, MessageText.ENDPOINT_NAME_REGISTER_DRONE);
        }

        [HttpPost("load-medications/{serialNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = MessageText.ENDPOINT_NAME_LOAD_MEDICATION)]
        public async Task<ActionResult<ApiResponse>> LoadMedicationsIntoDrone(string serialNumber, [FromBody] List<DroneMedicationDto> medications)
        {
            ApiResponse response = await _service.LoadMedicationsIntoDroneAsync(serialNumber, medications);
            return await HandleApiResponse(response, MessageText.ENDPOINT_NAME_LOAD_MEDICATION);
        }

        [HttpPost("chek-load-medications/{serialNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = MessageText.ENDPOINT_NAME_CKECK_LOAD_MEDICATION)]
        public async Task<ActionResult<ApiResponse>> CheckLoadMedicationsIntoDrone(string serialNumber)
        {
            ApiResponse response = await _service.CheckLoadedMedicationsIntoDroneAsync(serialNumber);
            return await HandleApiResponse(response, MessageText.ENDPOINT_NAME_LOAD_MEDICATION);
        }
    }
}
