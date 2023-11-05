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
                await GetDroneByIdAsync(drone.SerialNumber);
                if (_response.IsOK)
                {
                    throw new ArgumentException(MessageText.DRONE_SERIAL_NUMBER_DUPLICATED);
                }
                if (_response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _repositoryDrone.AddAsync(drone);
                    _response.AddCrateResponse204(drone);
                }
            }
            catch (Exception ex)
            {
                _response.AddBadResponse400(ex.Message);
            }
            return _response;
        }

        private async Task<ApiResponse> GetDroneByIdAsync(string serialNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(serialNumber))
                {
                    throw new ArgumentException(string.Format(MessageText.DRONE_SERIAL_NUMBER_EMPTY, serialNumber));
                }

                var drone = await _repositoryDrone.GetDroneByIdAsync(serialNumber);
                if (drone == null)
                {
                    _response.AddNotFoundResponse404(string.Format(MessageText.DRONE_NO_FOUND, serialNumber));
                    return _response;
                }
                _response.AddOkResponse200(drone);
            }
            catch (ArgumentException ex)
            {
                _response.AddBadResponse400(ex.Message);
            }
            catch (Exception ex)
            {
                _response.AddBadResponse400(ex.Message);
            }
            return _response;
        }
    }
}
