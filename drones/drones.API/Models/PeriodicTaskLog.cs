using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace drones.API.Models
{
    public class PeriodicTaskLog
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public double BatteryCapacity { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public PeriodicTaskLog()
        {
            Id = new Guid();
        }

        public override string ToString()
        {
            return $"Drone: {SerialNumber,-10} | BatteryCapacity: {BatteryCapacity,-3} | Date: {Date,-25}";
        }
    }
}
