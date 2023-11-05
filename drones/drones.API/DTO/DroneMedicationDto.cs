using drones.API.Utils;
using System.ComponentModel.DataAnnotations;

namespace drones.API.DTO
{
    public class DroneMedicationDto
    {
        [Key]
        [Required]
        [RegularExpression("^[A-Z0-9_]+$", ErrorMessage = MessageText.MEDICATION_CODE_FORMAT_VALIDATION)]
        public string Code { get; set; }

        [Required]
        [Range(1, 100)]
        public int Count { get; set; }
    }
}
