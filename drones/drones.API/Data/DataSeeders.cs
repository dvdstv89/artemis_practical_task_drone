using drones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace drones.API.Data
{
    public class DataSeeders
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
                    new Drone { SerialNumber = "1", Model = DroneModel.Middleweight, WeightLimit = 200, BatteryCapacity = 80, State = DroneState.IDLE },
                    new Drone { SerialNumber = "2", Model = DroneModel.Lightweight, WeightLimit = 300, BatteryCapacity = 70, State = DroneState.IDLE },
                    new Drone { SerialNumber = "3", Model = DroneModel.Lightweight, WeightLimit = 400, BatteryCapacity = 18, State = DroneState.RETURNING },
                    new Drone { SerialNumber = "4", Model = DroneModel.Lightweight, WeightLimit = 450, BatteryCapacity = 23, State = DroneState.RETURNING },
                    new Drone { SerialNumber = "5", Model = DroneModel.Lightweight, WeightLimit = 300, BatteryCapacity = 1, State = DroneState.IDLE },
                    new Drone { SerialNumber = "6", Model = DroneModel.Lightweight, WeightLimit = 400, BatteryCapacity = 5, State = DroneState.IDLE },
                    new Drone { SerialNumber = "7", Model = DroneModel.Lightweight, WeightLimit = 500, BatteryCapacity = 75, State = DroneState.IDLE },
                    new Drone { SerialNumber = "8", Model = DroneModel.Lightweight, WeightLimit = 500, BatteryCapacity = 80, State = DroneState.IDLE },
                    new Drone { SerialNumber = "9", Model = DroneModel.Lightweight, WeightLimit = 300, BatteryCapacity = 98, State = DroneState.IDLE },
                    new Drone { SerialNumber = "10", Model = DroneModel.Lightweight, WeightLimit = 350, BatteryCapacity = 100, State = DroneState.IDLE }
                );

                context.Medications.AddRange(
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
                );
               
                context.SaveChanges();
            }
        }
    }
}
