using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace drones.API.Models
{
    public class Drone
    {
        [Key]
        [Required]
        [StringLength(100)]
        public string SerialNumber { get; set; }

        [Required]
        [EnumDataType(typeof(DroneModel))]
        public DroneModel Model { get; set; }

        [Required]
        [Range(1, 500)]
        public double WeightLimit { get; set; }

        [Required]
        [Range(0, 100)]
        public double BatteryCapacity { get; set; }

        [Required]
        [EnumDataType(typeof(DroneState))]
        public DroneState State { get; set; }

        [JsonIgnore]
        public virtual ICollection<DroneMedication> DroneMedications { get; set; }

        public Drone()
        {
            DroneMedications = new List<DroneMedication>();
        }
    }

    public enum DroneState
    {
        IDLE = 1,
        LOADING = 2,
        LOADED = 3,
        DELIVERING = 4,
        DELIVERED = 5,
        RETURNING = 6
    }
    public enum DroneModel
    {
        Lightweight = 1,
        Middleweight = 2,
        Cruiserweight = 3,
        Heavyweight = 4
    }
}
