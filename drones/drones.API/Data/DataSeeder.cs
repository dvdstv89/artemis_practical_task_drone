using drones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace drones.API.Data
{
    public class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DroneApiDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<DroneApiDbContext>>()))
            {
                if (context.Drones.Any() || context.Medications.Any())
                {
                    return;
                }

                context.Drones.AddRange(
                    new Drone { SerialNumber = "DRN001", Model = DroneModel.Lightweight, WeightLimit = 100, BatteryCapacity = 80, State = DroneState.IDLE },
                    new Drone { SerialNumber = "DRN002", Model = DroneModel.Lightweight, WeightLimit = 175, BatteryCapacity = 70, State = DroneState.LOADED },
                    new Drone { SerialNumber = "DRN003", Model = DroneModel.Middleweight, WeightLimit = 250, BatteryCapacity = 18, State = DroneState.RETURNING },
                    new Drone { SerialNumber = "DRN004", Model = DroneModel.Middleweight, WeightLimit = 300, BatteryCapacity = 23, State = DroneState.RETURNING },
                    new Drone { SerialNumber = "DRN005", Model = DroneModel.Cruiserweight, WeightLimit = 350, BatteryCapacity = 40, State = DroneState.IDLE },
                    new Drone { SerialNumber = "DRN006", Model = DroneModel.Cruiserweight, WeightLimit = 400, BatteryCapacity = 5, State = DroneState.IDLE },
                    new Drone { SerialNumber = "DRN007", Model = DroneModel.Cruiserweight, WeightLimit = 325, BatteryCapacity = 75, State = DroneState.IDLE },
                    new Drone { SerialNumber = "DRN008", Model = DroneModel.Heavyweight, WeightLimit = 500, BatteryCapacity = 80, State = DroneState.IDLE },
                    new Drone { SerialNumber = "DRN009", Model = DroneModel.Heavyweight, WeightLimit = 480, BatteryCapacity = 98, State = DroneState.IDLE },
                    new Drone { SerialNumber = "DRN010", Model = DroneModel.Heavyweight, WeightLimit = 500, BatteryCapacity = 100, State = DroneState.IDLE }
                );

                context.Medications.AddRange(
                    new Medication { Code = "MED001", Name = "MedicationA", Weight = 80, Image = new byte[] { 255, 255, 255, 255 } },
                    new Medication { Code = "MED002", Name = "MedicationB", Weight = 60, Image = new byte[] { 255, 255, 255, 255 } },
                    new Medication { Code = "MED003", Name = "MedicationC", Weight = 45, Image = new byte[] { 255, 255, 255, 255 } },
                    new Medication { Code = "MED004", Name = "MedicationD", Weight = 97, Image = new byte[] { 255, 255, 255, 255 } },
                    new Medication { Code = "MED005", Name = "MedicationE", Weight = 38, Image = new byte[] { 255, 255, 255, 255 } },
                    new Medication { Code = "MED006", Name = "MedicationF", Weight = 100, Image = new byte[] { 255, 255, 255, 255 } },
                    new Medication { Code = "MED007", Name = "MedicationG", Weight = 50, Image = new byte[] { 255, 255, 255, 255 } },
                    new Medication { Code = "MED008", Name = "MedicationH", Weight = 75, Image = new byte[] { 255, 255, 255, 255 } },
                    new Medication { Code = "MED009", Name = "MedicationI", Weight = 57, Image = new byte[] { 255, 255, 255, 255 } },
                    new Medication { Code = "MED010", Name = "MedicationJ", Weight = 40, Image = new byte[] { 255, 255, 255, 255 } }
                );

                context.DroneMedication.AddRange(
                    new DroneMedication { DroneSerialNumber = "DRN002", MedicationCode = "MED001", Count = 1 },
                    new DroneMedication { DroneSerialNumber = "DRN002", MedicationCode = "MED002", Count = 1 }
               );

                context.SaveChanges();
            }
        }
    }
}
