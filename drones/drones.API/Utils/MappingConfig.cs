using AutoMapper;
using drones.API.DTO;
using drones.API.Models;

namespace drones.API.Utils
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<DroneMedication, DroneMedicationDto>().ReverseMap();
            CreateMap<DroneMedication, DroneMedicationCheckDto>().ReverseMap();
            CreateMap<Drone, DroneBatteryLevelDto>().ReverseMap();
        }
    }
}
