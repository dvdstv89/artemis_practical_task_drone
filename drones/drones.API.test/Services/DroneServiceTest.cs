using drones.API.DTO;
using drones.API.Models;
using drones.API.Services;
using System.Net;

namespace drones.API.test.Services
{
    internal class DroneServiceTest : BaseTest
    {      

        [Test]
        [TestCase(HttpStatusCode.Created, TestName = "Register drone Ok")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Register drone whit duplicate serial number")]
        public async Task RegisterDroneTest(HttpStatusCode statusCodeResult)
        {
            //Arrange
            Drone newDrone = new Drone()
            {
                SerialNumber = "11",
                Model = DroneModel.Middleweight,
                WeightLimit = 200,
                BatteryCapacity = 80,
                State = DroneState.IDLE
            };
            InitializeDefaultContext();
            IDroneService droneService = new DroneService(droneRepository, medicationRepository, mapper);

            //Act    
            if (TestContext.CurrentContext.Test.Name == "Register drone whit duplicate serial number")
            {
                newDrone.SerialNumber = "1";
            }

            var response = await droneService.RegisterDroneAsync(newDrone);
            string result = string.Join("\n", response.Errors);
            Console.WriteLine(result);

            //Assert
            Assert.AreEqual(statusCodeResult, response.StatusCode);
        }

        [Test]       
        [TestCase(HttpStatusCode.NotFound, TestName = "Load medication into drone whit not found available drone BUSY")]
        [TestCase(HttpStatusCode.OK, TestName = "Load medication into drone Ok")]
        [TestCase(HttpStatusCode.NotFound, TestName = "Load medication into drone whit not found available drone BATTERY LOW")]
        [TestCase(HttpStatusCode.NotFound, TestName = "Load medication into drone whit not found medication")]       
        [TestCase(HttpStatusCode.BadRequest, TestName = "Load medication into drone whit weight limit exceded")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Load medication into drone whit empty serial number")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Load medication into drone whit empty medications")]
        public async Task LoadMedicationsIntoDroneTest(HttpStatusCode statusCodeResult)
        {
            //Arrange
            string serialNumber = "1";
            List<DroneMedicationDto> medicationsToLoadList = new List<DroneMedicationDto>
            {
                 new DroneMedicationDto { Code = "M1", Count = 1 },
                 new DroneMedicationDto { Code = "M2", Count = 1 },
                 new DroneMedicationDto { Code = "M3", Count = 1 }
            };                    

            //Act    
            if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit not found available drone BUSY")
            {
                serialNumber = "4";            
            }
            else if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit not found available drone BATTERY LOW")
            {
                serialNumber = "6";               
            }
            else if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit not found medication")
            {
                serialNumber = "2";
                medicationsToLoadList.Add(new DroneMedicationDto { Code = "M15", Count = 1 });
            }
            else if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit empty serial number")
            {
                serialNumber = "";             
            }
            else if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit weight limit exceded")
            {
                serialNumber = "10";               
            }
            else if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit empty medications")
            {               
                medicationsToLoadList = null;
            }

            InitializeDefaultContext();
            IDroneService droneService = new DroneService(droneRepository, medicationRepository, mapper);
            var response = await droneService.LoadMedicationsIntoDroneAsync(serialNumber, medicationsToLoadList);
            string result = string.Join("\n", response.Errors);
            Console.WriteLine(result);

            //Assert
            Assert.AreEqual(statusCodeResult, response.StatusCode);
        }

        [Test]
        [TestCase(HttpStatusCode.OK, TestName = "Check loaded medication into the drone OK")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Check loaded medication into the drone whit empty serial number")]
        [TestCase(HttpStatusCode.NotFound, TestName = "Check loaded medication into the drone whit not found drone")]
        [TestCase(HttpStatusCode.NotFound, TestName = "Check loaded medication into the drone whit not found medications")]
        public async Task CheckLoadedMedicationsIntoDroneTest(HttpStatusCode statusCodeResult)
        {
            //Arrange
            string serialNumber = "2";
            InitializeDefaultContext();
            IDroneService droneService = new DroneService(droneRepository, medicationRepository,  mapper);

            //Act      
            if (TestContext.CurrentContext.Test.Name == "Check loaded medication into the drone whit empty serial number")
            {
                serialNumber = "";
            }
            else if (TestContext.CurrentContext.Test.Name == "Check loaded medication into the drone whit not found drone")
            {
                serialNumber = "100";
            }
            else if (TestContext.CurrentContext.Test.Name == "Check loaded medication into the drone whit not found medications")
            {
                serialNumber = "3";
            }

            var response = await droneService.CheckLoadedMedicationsIntoDroneAsync(serialNumber);
            string result = string.Join("\n", response.Errors);
            Console.WriteLine(result);

            //Assert
            Assert.AreEqual(statusCodeResult, response.StatusCode);
        }

        [Test]
        [TestCase(HttpStatusCode.OK, TestName = "Check availables drones for loading OK")]     
        public async Task CheckAvailableForLoadingTest(HttpStatusCode statusCodeResult)
        {
            //Arrange
            InitializeDefaultContext();
            IDroneService droneService = new DroneService(droneRepository, medicationRepository, mapper);

            //Act  
            var response = await droneService.GetDronesAvailablesForLoadingAsync();
            string result = string.Join("\n", response.Errors);
            Console.WriteLine(result);
            //Assert
            Assert.AreEqual(statusCodeResult, response.StatusCode);
        }

        [Test]
        [TestCase(HttpStatusCode.OK, TestName = "Check battery capacity OK")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Check battery capacity whit empty serial number")]
        [TestCase(HttpStatusCode.NotFound, TestName = "Check battery capacity whit not found drone")]
        public async Task CheckBatteryCapacityTest(HttpStatusCode statusCodeResult)
        {
            //Arrange
            string serialNumber = "1";
            InitializeDefaultContext();
            IDroneService droneService = new DroneService(droneRepository, medicationRepository, mapper);

            //Act       
            if (TestContext.CurrentContext.Test.Name == "Check battery capacity whit empty serial number")
            {
                serialNumber = "";
            }
            else if (TestContext.CurrentContext.Test.Name == "Check battery capacity whit not found drone")
            {
                serialNumber = "100";
            }

            var response = await droneService.CheckBatteryCapacityAsync(serialNumber);
            string result = string.Join("\n", response.Errors);
            Console.WriteLine(result);

            //Assert
            Assert.AreEqual(statusCodeResult, response.StatusCode);
        }

        [Test]
        [TestCase(HttpStatusCode.OK, TestName = "Change battery capacity OK")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Change battery capacity whit empty serial number")]
        [TestCase(HttpStatusCode.NotFound, TestName = "Change battery capacity whit not found drone")]
        public async Task ChangeBatteryLevelTest(HttpStatusCode statusCodeResult)
        {
            //Arrange
            string serialNumber = "1";
            DroneBatteryLevelDto drone = new DroneBatteryLevelDto() { BatteryCapacity = 50 };
            InitializeDefaultContext();
            IDroneService droneService = new DroneService(droneRepository, medicationRepository, mapper);

            //Act           
            if (TestContext.CurrentContext.Test.Name == "Change battery capacity whit empty serial number")
            {
                serialNumber = "";
            }
            else if (TestContext.CurrentContext.Test.Name == "Change battery capacity whit not found drone")
            {
                serialNumber = "100";
            }


            var response = await droneService.ChangeBatteryLevelAsync(serialNumber, drone);
            string result = string.Join("\n", response.Errors);
            Console.WriteLine(result);

            //Assert
            Assert.AreEqual(statusCodeResult, response.StatusCode);
        }

        [Test]
        [TestCase(HttpStatusCode.OK, TestName = "Change drone state OK")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Change drone state whit empty serial number")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Change drone state whit to LOADING with battery low")]
        [TestCase(HttpStatusCode.NotFound, TestName = "Change drone state whit not found drone")]
        public async Task ChangeStateTest(HttpStatusCode statusCodeResult)
        {
            //Arrange
            string serialNumber = "1";
            DroneStateDto drone = new DroneStateDto() { State = DroneState.LOADED };
            InitializeDefaultContext();
            IDroneService droneService = new DroneService(droneRepository, medicationRepository, mapper);

            //Act
            if (TestContext.CurrentContext.Test.Name == "Change drone state whit empty serial number")
            {
                serialNumber = "";
            }
            else if (TestContext.CurrentContext.Test.Name == "Change drone state whit to LOADING with battery low")
            {
                serialNumber = "5";
                drone.State = DroneState.LOADING;
            }
            else if (TestContext.CurrentContext.Test.Name == "Change drone state whit not found drone")
            {
                serialNumber = "100";
            }


            var response = await droneService.ChangeStateyAsync(serialNumber, drone);
            string result = string.Join("\n", response.Errors);
            Console.WriteLine(result);

            //Assert
            Assert.AreEqual(statusCodeResult, response.StatusCode);
        }

    }
}
