using drones.API.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace drones.API.Models
{
    public class Medication
    {
        [Key]
        [Required]
        [RegularExpression("^[A-Z0-9_]+$", ErrorMessage = MessageText.MEDICATION_CODE_FORMAT_VALIDATION)]
        public string Code { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]+$", ErrorMessage = MessageText.MEDICATION_NAME_FORMAT_VALIDATION)]
        public string Name { get; set; }

        [Required]
        [Range(1, 500, ErrorMessage = MessageText.MEDICATION_WEIGHT_MIN_VALUE_VALIDATION)]
        public double Weight { get; set; }

        public byte[] Image { get; set; }

        [JsonIgnore]
        public virtual ICollection<DroneMedication> DroneMedications { get; set; }

        public Medication()
        {
            DroneMedications = new List<DroneMedication>();
        }
    }
}
