using AutoMapper;
using drones.API.DTO;
using drones.API.Models;
using drones.API.Repositories;
using drones.API.Utils;

namespace drones.API.Services
{
    public interface IDroneService
    {
        Task<ApiResponse> RegisterDroneAsync(Drone drone);
        Task<ApiResponse> LoadMedicationsIntoDroneAsync(string serialNumber, List<DroneMedicationDto> medications);
        Task<ApiResponse> CheckLoadedMedicationsIntoDroneAsync(string serialNumber);
        Task<ApiResponse> GetDronesAvailablesForLoadingAsync();
    }

    public class DroneService : IDroneService
    {
        private readonly IDroneRepository _droneRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IDroneMedicationRepository _droneMedicationRepository;
        private readonly IMapper _mapper;
        private ApiResponse _response;

        public DroneService(IDroneRepository droneRepository, IMedicationRepository medicationRepository, IDroneMedicationRepository droneMedicationRepository, IMapper mapper)
        {
            _response = new ApiResponse();
            _droneRepository = droneRepository;
            _medicationRepository = medicationRepository;
            _droneMedicationRepository = droneMedicationRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> RegisterDroneAsync(Drone drone)
        {
            try
            {
                await GetDroneByIdAsync(drone.SerialNumber);
                if (_response.IsOK)
                {
                    throw new ArgumentException(string.Format(MessageText.DRONE_SERIAL_NUMBER_DUPLICATED, drone.SerialNumber));
                }
                if (_response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _droneRepository.AddAsync(drone);
                    _response.AddCrateResponse204(drone);
                }
            }
            catch (Exception ex)
            {
                _response.AddBadResponse400(ex.Message);
            }
            return _response;
        }

        public async Task<ApiResponse> LoadMedicationsIntoDroneAsync(string serialNumber, List<DroneMedicationDto> medications)
        {
            try
            {
                if (medications == null)
                {
                    throw new ArgumentException(MessageText.MEDICATIONS_EMPTY);
                }
                await GetDroneAvailableForLoadingAsync(serialNumber);
                if (!_response.IsOK)
                {
                    return _response;
                }

                Drone drone = (Drone)_response.Result;

                double totalWeight = 0;
                foreach (var medicationDto in medications)
                {
                    Medication medication = await _medicationRepository.GetMedicationByIdAsync(medicationDto.Code);
                    if (medication == null)
                    {
                        _response.AddNotFoundResponse404(string.Format(MessageText.MEDICATION_NOT_FOUND, medicationDto.Code));
                        return _response;
                    }

                    DroneMedication droneMedicationExitent = drone.DroneMedications.FirstOrDefault(dr => dr.MedicationCode == medicationDto.Code);
                    if (droneMedicationExitent != null)
                    {
                        droneMedicationExitent.Count += medicationDto.Count;
                    }
                    else
                    {
                        var droneMedication = new DroneMedication
                        {
                            DroneSerialNumber = serialNumber,
                            MedicationCode = medicationDto.Code,
                            Count = medicationDto.Count
                        };
                        drone.DroneMedications.Add(droneMedication);
                    }

                    totalWeight += medication.Weight * medicationDto.Count;
                }

                if (drone.WeightLimit < totalWeight)
                {
                    throw new ArgumentException(string.Format(MessageText.DRONE_CARGO_WEIGHT_EXCEDED, drone.WeightLimit, totalWeight));
                }
                drone.State = DroneState.LOADED;
                await LoadDroneAsync(drone);
                _response.AddOkResponse200(MessageText.DRONE_LOADED);
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

        public async Task<ApiResponse> CheckLoadedMedicationsIntoDroneAsync(string serialNumber)
        {
            try
            {
                await GetDroneByIdAsync(serialNumber);
                if (_response.IsOK)
                {
                    Drone drone = (Drone)_response.Result;
                    IEnumerable<DroneMedicationCheckDto> medicationDtos = _mapper.Map<IEnumerable<DroneMedicationCheckDto>>(drone.DroneMedications);
                    foreach (var item in medicationDtos)
                    {
                        item.Medication = await _medicationRepository.GetMedicationByIdAsync(item.MedicationCode);
                    }

                    _response.AddOkResponse200(medicationDtos);
                }
            }
            catch (Exception ex)
            {
                _response.AddBadResponse400(ex.Message);
            }
            return _response;
        }

        public async Task<ApiResponse> GetDronesAvailablesForLoadingAsync()
        {
            try
            {
                var drones = await _droneRepository.CheckAvailableForLoadingAsync();
                if (!drones.Any())
                {
                    _response.AddNotFoundResponse404(MessageText.DRONE_NO_FOUND_AVAILABLES_FOR_LOADING);
                }
                else
                {
                    _response.AddOkResponse200(drones);
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
                    throw new ArgumentException(MessageText.DRONE_SERIAL_NUMBER_EMPTY);
                }

                var drone = await _droneRepository.GetDroneByIdAsync(serialNumber);
                if (drone == null)
                {
                    _response.AddNotFoundResponse404(string.Format(MessageText.DRONE_NOT_FOUND, serialNumber));
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

        private async Task<ApiResponse> GetDroneAvailableForLoadingAsync(string serialNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(serialNumber))
                {
                    throw new ArgumentException(MessageText.DRONE_SERIAL_NUMBER_EMPTY);
                }
                var result = await GetDroneByIdAsync(serialNumber);
                if (!result.IsOK)
                {
                    return result;
                }

                Drone drone = (Drone)result.Result;
                if (drone.State != DroneState.IDLE)
                {
                    _response.AddNotFoundResponse404(string.Format(MessageText.DRONE_STATE_NO_READY_TO_FLY_BUSY, serialNumber, drone.State.ToString()));
                    return _response;
                }
                if (drone.BatteryCapacity < 25)
                {
                    _response.AddNotFoundResponse404(string.Format(MessageText.DRONE_STATE_NO_READY_TO_FLY_BATTERY_LOW, serialNumber, drone.BatteryCapacity));
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

        private async Task LoadDroneAsync(Drone drone)
        {
            try
            {
                IEnumerable<DroneMedication> droneMedications = drone.DroneMedications;
                foreach (var droneMedication in droneMedications)
                {
                    await _droneMedicationRepository.AddAsync(droneMedication);
                }
                await _droneRepository.UpdateAsync(drone);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
