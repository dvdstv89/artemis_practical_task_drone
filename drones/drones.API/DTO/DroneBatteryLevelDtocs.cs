using System.ComponentModel.DataAnnotations;

namespace drones.API.DTO
{
    public class DroneBatteryLevelDto
    {
        [Required]
        [Range(0, 100)]
        public double BatteryCapacity { get; set; }
    }
}
