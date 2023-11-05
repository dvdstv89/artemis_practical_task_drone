using drones.API.Models;
using drones.API.Services;
using System.Net;

namespace drones.API.test.Services
{
    internal class DroneServiceTest : BaseTest
    {
        private IDroneService droneService;
        private Drone newDrone;

        [SetUp]
        public void Setup()
        {
            newDrone = new Drone()
            {
                SerialNumber = "11",
                Model = DroneModel.Middleweight,
                WeightLimit = 200,
                BatteryCapacity = 80,
                State = DroneState.IDLE
            };

            InitializeContext();
            droneService = new DroneService(droneRepository, medicationRepository, droneMedicationRepository);
        }

        [Test]
        [TestCase(HttpStatusCode.Created, TestName = "Register drone Ok")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Register drone whit duplicate serial number")]
        public async Task RegisterDroneService(HttpStatusCode statusCodeResult)
        {
            if (statusCodeResult == HttpStatusCode.BadRequest)
            {
                newDrone.SerialNumber = "1";
            }
            var response = await droneService.RegisterDroneAsync(newDrone);
            string result = string.Join("\n", response.Errors);
            Console.WriteLine(result);
            Assert.AreEqual(statusCodeResult, response.StatusCode);
        }

    }
}
