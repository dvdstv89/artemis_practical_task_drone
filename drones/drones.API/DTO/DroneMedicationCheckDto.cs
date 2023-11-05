using drones.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace drones.API.DTO
{
    public class DroneMedicationCheckDto
    {
        [Required]
        [Range(1, 100)]
        public int Count { get; set; }


        [JsonIgnore]
        public string MedicationCode { get; set; }

        public Medication Medication { get; set; }
    }
}
