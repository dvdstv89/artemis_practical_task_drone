using drones.API.Data;
using drones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace drones.API.Repositories
{
    public interface IDroneRepository : IBaseRepository<Drone>
    {
        Task<Drone> GetDroneByIdAsync(string serialNumber);
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
                .FirstOrDefaultAsync(d => d.SerialNumber == serialNumber);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
