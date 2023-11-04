using System.ComponentModel.DataAnnotations;

namespace drones.API.Models
{
    public class Medication
    {

        [Key]
        [Required]
        [RegularExpression("^[A-Z0-9_]+$")]
        public string Code { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]+$")]
        public string Name { get; set; }

        [Required]
        [Range(0.1, 200)]
        public double Weight { get; set; }

        public byte[] Image { get; set; }
        
        public virtual ICollection<DroneMedication> DroneMedications { get; set; }

        public Medication()
        {
            DroneMedications = new List<DroneMedication>();
        }
    }
}
