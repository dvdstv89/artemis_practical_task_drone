using drones.API.Models;
using drones.API.Repositories;
using drones.API.Utils;

namespace drones.API.Services
{
    public interface IPeriodicTaskLogService
    {
        Task<ApiResponse> PeriodicTaskLogs();
        Task<ApiResponse> GetAllsByIdAsync(string serialNumber);
    }

    public class PeriodicTaskLogService : IPeriodicTaskLogService
    {
        private ApiResponse _response;
        private readonly IPeriodicTaskLogRepository _periodicTaskLogRepository;
        private readonly IDroneService _droneService;
        public PeriodicTaskLogService(IPeriodicTaskLogRepository periodicTaskLogRepository, IDroneService droneService)
        {
            _periodicTaskLogRepository = periodicTaskLogRepository;
            _response = new ApiResponse();
            _droneService = droneService;
        }

        public async Task<ApiResponse> GetAllsByIdAsync(string serialNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(serialNumber))
                {
                    throw new ArgumentException(MessageText.DRONE_SERIAL_NUMBER_EMPTY);
                }
                var result = await _periodicTaskLogRepository.GetLogByIdAsync(serialNumber);
                if (result == null)
                {
                    _response.AddNotFoundResponse404(string.Format(MessageText.DRONE_LOG_NOT_FOUND, serialNumber));
                }
                else
                {
                    _response.AddOkResponse200(result);
                }
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

        public async Task<ApiResponse> PeriodicTaskLogs()
        {
            try
            {
                ApiResponse droneApiResponse = await _droneService.GetAllDroneAsync();
                if (droneApiResponse.IsValid)
                {
                    var drones = (IEnumerable<Drone>)droneApiResponse.Result;
                    if (drones == null)
                    {
                        throw new ArgumentException(MessageText.DRONE_NOT_FOUND_EMPTY_DB);
                    }
                    else
                    {
                        return await AddListAsync(drones);
                    }
                }
                return droneApiResponse;
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

        private async Task<ApiResponse> AddListAsync(IEnumerable<Drone> drones)
        {
            try
            {
                List<string> messages = new List<string>();
                foreach (var drone in drones)
                {
                    await AddAsync(drone);
                    if (_response.IsValid)
                    {
                        messages.Add((string)_response.Result);
                    }
                    else
                    {
                        messages.Add(string.Join(" && ", _response.Errors));
                    }
                }
                string logMessage = string.Join("\n      ", messages);
                _response.AddOkResponse200(logMessage);
            }
            catch (Exception ex)
            {
                _response.AddBadResponse400(ex.Message);
            }
            return _response;
        }

        private async Task<ApiResponse> AddAsync(Drone drone)
        {
            try
            {
                PeriodicTaskLog droneBatteryLog = new PeriodicTaskLog
                {
                    SerialNumber = drone.SerialNumber,
                    BatteryCapacity = drone.BatteryCapacity,
                    Date = DateTime.Now
                };
                await _periodicTaskLogRepository.AddAsync(droneBatteryLog);
                _response.AddOkResponse200(droneBatteryLog.ToString());
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
