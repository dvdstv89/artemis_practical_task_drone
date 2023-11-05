using drones.API.Data;
using drones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace drones.API.Repositories
{
    public interface IDroneRepository : IBaseRepository<Drone>
    {
        Task<Drone> GetDroneByIdAsync(string serialNumber);
        Task<IEnumerable<Drone>> CheckAvailableForLoadingAsync();
    }

    public class DroneRepository : BaseRepository<Drone>, IDroneRepository
    {
        public DroneRepository(DroneApiDbContext context) : base(context) { }

        public async Task<Drone> GetDroneByIdAsync(string serialNumber)
        {
            try
            {
                return await _context.Drones
                   .Include(d => d.DroneMedications)
                   .ThenInclude(dm => dm.Medication)
                   .Where(d => d.SerialNumber == serialNumber)                  
                   .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Drone>> CheckAvailableForLoadingAsync()
        {
            try
            {
                return await _context.Drones
                    .Where(d => d.State == DroneState.IDLE && d.BatteryCapacity > 25)                    
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
