using drones.API.Models;
using System.ComponentModel.DataAnnotations;

namespace drones.API.DTO
{
    public class DroneStateDto
    {
        [Required]
        [EnumDataType(typeof(DroneState))]
        public DroneState State { get; set; }
    }
}
