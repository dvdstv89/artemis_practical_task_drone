using System.ComponentModel.DataAnnotations;

namespace drones.API.Models
{
    public class DroneMedication
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]
        [Range(1, 100)]
        public int Count { get; set; }

        [Required]
        public string DroneSerialNumber { get; set; }

        [Required]
        public string MedicationCode { get; set; }

        public DroneMedication()
        {
            Id = Guid.NewGuid();
        }
    }
}
