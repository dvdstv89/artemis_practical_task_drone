using drones.API.Models;
using drones.API.Repositories;
using drones.API.Utils;

namespace drones.API.Services
{
    public interface IDroneService
    {
        Task<ApiResponse> RegisterDroneAsync(Drone drone);
    }

    public class DroneService : IDroneService
    {
        private readonly IDroneRepository _repositoryDrone;
        private ApiResponse _response;

        public DroneService(IDroneRepository droneRepository)
        {
            _response = new ApiResponse();
            _repositoryDrone = droneRepository;
        }

        public async Task<ApiResponse> RegisterDroneAsync(Drone drone)
        {
            try
            {
                _response.AddOkResponse200(drone);
            }
            catch (Exception ex)
            {
                _response.AddBadResponse400(ex.Message);
            }
            return _response;
        }
    }
}
