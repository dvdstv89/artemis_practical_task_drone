using drones.API.Data;
using drones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace drones.API.Repositories
{
    public interface IMedicationRepository : IBaseRepository<Medication>
    {
        Task<Medication> GetMedicationByIdAsync(string code);
    }

    public class MedicationRepository : BaseRepository<Medication>, IMedicationRepository
    {
        public MedicationRepository(DroneApiDbContext context) : base(context) { }

        public async Task<Medication> GetMedicationByIdAsync(string code)
        {
            try
            {
                return await _context.Medications
                    .Where(d => d.Code == code)                    
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
