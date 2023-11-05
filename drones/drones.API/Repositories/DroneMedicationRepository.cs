using drones.API.Data;
using drones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace drones.API.Repositories
{
    public interface IDroneMedicationRepository : IBaseRepository<DroneMedication>
    {
        
    }

    public class DroneMedicationRepository : BaseRepository<DroneMedication>, IDroneMedicationRepository
    {
        public DroneMedicationRepository(DroneApiDbContext context) : base(context) { }

    }
}
