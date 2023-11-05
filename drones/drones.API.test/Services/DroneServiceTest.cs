﻿using drones.API.DTO;
using drones.API.Models;
using drones.API.Services;
using System.Net;

namespace drones.API.test.Services
{


    internal class DroneServiceTest : BaseTest
    {
        private IDroneService droneService;
        private Drone newDrone;
        private List<DroneMedicationDto> medicationsToLoadList;

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

            medicationsToLoadList = new List<DroneMedicationDto>
            {
                 new DroneMedicationDto { Code = "M1", Count = 1 },
                 new DroneMedicationDto { Code = "M2", Count = 1 },
                 new DroneMedicationDto { Code = "M3", Count = 1 }
            };

            InitializeContext();
            droneService = new DroneService(droneRepository, medicationRepository, droneMedicationRepository, mapper);
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

        [Test]
        [TestCase(HttpStatusCode.OK, TestName = "Load medication into drone Ok")]
        [TestCase(HttpStatusCode.NotFound, TestName = "Load medication into drone whit not found available drone BUSY")]
        [TestCase(HttpStatusCode.NotFound, TestName = "Load medication into drone whit not found available drone BATTERY LOW")]
        [TestCase(HttpStatusCode.NotFound, TestName = "Load medication into drone whit not found medication")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Load medication into drone whit empty serial number")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Load medication into drone whit empty medications")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Load medication into drone whit weight limit exceded")]
        public async Task LoadMedicationsIntoDrone(HttpStatusCode statusCodeResult)
        {
            string serialNumber = "1";
            List<DroneMedicationDto> medications = null;

            if (TestContext.CurrentContext.Test.Name == "Load medication into drone Ok")
            {
                medications = medicationsToLoadList;
            }
            else if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit not found available drone BUSY")
            {
                serialNumber = "4";
                medications = medicationsToLoadList;
            }
            else if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit not found available drone BATTERY LOW")
            {
                serialNumber = "6";
                medications = medicationsToLoadList;
            }
            else if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit not found medication")
            {
                serialNumber = "2";
                medications = medicationsToLoadList;
                medications.Add(new DroneMedicationDto { Code = "M15", Count = 1 });
            }
            else if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit empty serial number")
            {
                serialNumber = "";
                medications = medicationsToLoadList;
            }
            else if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit empty medications")
            {
                // notthing to do
            }
            else if (TestContext.CurrentContext.Test.Name == "Load medication into drone whit weight limit exceded")
            {
                serialNumber = "10";
                medications = medicationsToLoadList;
            }

            var response = await droneService.LoadMedicationsIntoDroneAsync(serialNumber, medications);
            string result = string.Join("\n", response.Errors);
            Console.WriteLine(result);
            Assert.AreEqual(statusCodeResult, response.StatusCode);
        }

        [Test]
        [TestCase(HttpStatusCode.OK, TestName = "Check loaded medication into the drone OK")]
        [TestCase(HttpStatusCode.BadRequest, TestName = "Check loaded medication into the drone whit empty serial number")]
        [TestCase(HttpStatusCode.NotFound, TestName = "Check loaded medication into the drone whit not found drone")]
        public async Task CheckLoadedMedicationsIntoDroneAsync(HttpStatusCode statusCodeResult)
        {
            string serialNumber = "1";

            if (TestContext.CurrentContext.Test.Name == "Check loaded medication into the drone OK")
            {
                // notthing to do
            }
            else if (TestContext.CurrentContext.Test.Name == "Check loaded medication into the drone whit empty serial number")
            {
                serialNumber = "";
            }
            else if (TestContext.CurrentContext.Test.Name == "Check loaded medication into the drone whit not found drone")
            {
                serialNumber = "100";
            }

            var response = await droneService.CheckLoadedMedicationsIntoDroneAsync(serialNumber);
            string result = string.Join("\n", response.Errors);
            Console.WriteLine(result);
            Assert.AreEqual(statusCodeResult, response.StatusCode);
        }

    }
}
