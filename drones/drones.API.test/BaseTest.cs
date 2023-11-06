using AutoMapper;
using drones.API.Data;
using drones.API.DTO;
using drones.API.Models;
using drones.API.Repositories;
using drones.API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace drones.API.test
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DroneMedication, DroneMedicationDto>().ReverseMap();
            CreateMap<DroneMedication, DroneMedicationCheckDto>().ReverseMap();
            CreateMap<Drone, DroneBatteryLevelDto>().ReverseMap();
            CreateMap<Drone, DroneStateDto>().ReverseMap();
        }
    }

    public class BaseTest
    {
        protected IMapper mapper;
        protected IDroneRepository droneRepository;
        protected IMedicationRepository medicationRepository;

        protected List<Drone> dronesList = new List<Drone>
        {
                 new Drone { SerialNumber = "1", Model = DroneModel.Middleweight, WeightLimit = 200, BatteryCapacity = 80, State = DroneState.IDLE },
                 new Drone { SerialNumber = "2", Model = DroneModel.Lightweight, WeightLimit = 300, BatteryCapacity = 70, State = DroneState.IDLE },
                 new Drone { SerialNumber = "3", Model = DroneModel.Lightweight, WeightLimit = 400, BatteryCapacity = 18, State = DroneState.RETURNING },
                 new Drone { SerialNumber = "4", Model = DroneModel.Lightweight, WeightLimit = 450, BatteryCapacity = 33, State = DroneState.RETURNING },
                 new Drone { SerialNumber = "5", Model = DroneModel.Lightweight, WeightLimit = 300, BatteryCapacity = 1, State = DroneState.IDLE },
                 new Drone { SerialNumber = "6", Model = DroneModel.Lightweight, WeightLimit = 400, BatteryCapacity = 5, State = DroneState.IDLE },
                 new Drone { SerialNumber = "7", Model = DroneModel.Lightweight, WeightLimit = 500, BatteryCapacity = 75, State = DroneState.IDLE },
                 new Drone { SerialNumber = "8", Model = DroneModel.Lightweight, WeightLimit = 500, BatteryCapacity = 80, State = DroneState.IDLE },
                 new Drone { SerialNumber = "9", Model = DroneModel.Lightweight, WeightLimit = 300, BatteryCapacity = 98, State = DroneState.IDLE },
                 new Drone { SerialNumber = "10", Model = DroneModel.Lightweight, WeightLimit = 100, BatteryCapacity = 100, State = DroneState.IDLE }
        };       

        protected List<Medication> medicationsList = new List<Medication>
        {
                 new Medication { Code = "M1", Name = "Med1", Weight = 80, Image = new byte[] { 255, 255, 255, 255 } },
                 new Medication { Code = "M2", Name = "Med2", Weight = 60, Image = new byte[] { 255, 255, 255, 255 } },
                 new Medication { Code = "M3", Name = "Med3", Weight = 45, Image = new byte[] { 255, 255, 255, 255 } },
                 new Medication { Code = "M4", Name = "Med4", Weight = 97, Image = new byte[] { 255, 255, 255, 255 } },
                 new Medication { Code = "M5", Name = "Med5", Weight = 38, Image = new byte[] { 255, 255, 255, 255 } },
                 new Medication { Code = "M6", Name = "Med6", Weight = 100, Image = new byte[] { 255, 255, 255, 255 } },
                 new Medication { Code = "M7", Name = "Med7", Weight = 50, Image = new byte[] { 255, 255, 255, 255 } },
                 new Medication { Code = "M8", Name = "Med8", Weight = 75, Image = new byte[] { 255, 255, 255, 255 } },
                 new Medication { Code = "M9", Name = "Med9", Weight = 57, Image = new byte[] { 255, 255, 255, 255 } },
                 new Medication { Code = "M10", Name = "Med10", Weight = 40, Image = new byte[] { 255, 255, 255, 255 } }
        };

        protected List<DroneMedication> droneMedicationList = new List<DroneMedication>
        {
                 new  DroneMedication  { DroneSerialNumber = "2", MedicationCode = "M1", Count = 1 },
                 new  DroneMedication  { DroneSerialNumber = "2", MedicationCode = "M2", Count = 1 },
        };

        [SetUp]
        public void Setup()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            mapper = configuration.CreateMapper();
        }       

        protected void InitializeDefaultContext()
        {
            var nameDb = Guid.NewGuid().ToString();
            var services = new ServiceCollection();
            services.AddScoped<DroneApiDbContext>(provider =>
            {
                var options = new DbContextOptionsBuilder<DroneApiDbContext>()
                    .UseInMemoryDatabase(nameDb)
                    .EnableSensitiveDataLogging()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .Options;
                return new DroneApiDbContext(options);
            });

            var _serviceProvider = services.BuildServiceProvider();
            var db = _serviceProvider.GetService<DroneApiDbContext>();
            db.AddRange(dronesList);
            db.AddRange(medicationsList);
            db.AddRange(droneMedicationList);
            db.SaveChanges();
            droneRepository = new DroneRepository(db);
            medicationRepository = new MedicationRepository(db);
        }

        protected ApiResponse HandleApiResponse(ActionResult<ApiResponse> response)
        {

            if (response.Result is ObjectResult objectResult)
            {
                if (objectResult.Value is ApiResponse)
                {
                    return (ApiResponse)objectResult.Value;
                }
                else if (objectResult.Value is SerializableError serializableError)
                {
                    var apiResponse = new ApiResponse();
                    foreach (var key in serializableError.Keys)
                    {
                        var values = serializableError[key] as string[];
                        if (values != null && values.Length > 0)
                        {
                            foreach (var value in values)
                            {
                                apiResponse.AddBadResponse400($"{key}: {value}");
                            }
                        }
                    }
                    return apiResponse;
                }
            }
            return null;
        }
    }
}
