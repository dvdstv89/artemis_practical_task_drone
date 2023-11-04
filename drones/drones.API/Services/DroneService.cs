using drones.API.Models;
using drones.API.Utils;

namespace drones.API.Services
{
    public interface IDroneService
    {
        Task<ApiResponse> RegisterDroneAsync(Drone drone);
    }

    public class DroneService : IDroneService
    {
        private ApiResponse _response;

        public DroneService()
        {
            _response = new ApiResponse();
        }

        public async Task<ApiResponse> RegisterDroneAsync(Drone drone)
        {
            try
            {
                _response.IsOK = true;
                _response.Result = drone;
                _response.StatusCode = System.Net.HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsOK = false;
                _response.Errors.Add(ex.Message);
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return _response;
        }
    }
}
