using drones.API.Data;
using drones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace drones.API.Repositories
{
    public interface IPeriodicTaskLogRepository : IBaseRepository<PeriodicTaskLog>
    {
        Task<IEnumerable<PeriodicTaskLog>> GetLogByIdAsync(string serialNumber);
    }

    public class PeriodicTaskLogRepository : BaseRepository<PeriodicTaskLog>, IPeriodicTaskLogRepository
    {
        public PeriodicTaskLogRepository(DroneApiDbContext context) : base(context) { }

        public async Task<IEnumerable<PeriodicTaskLog>> GetLogByIdAsync(string serialNumber)
        {
            try
            {
                return await _context.PeriodicTaskLogs
                    .Where(d => d.SerialNumber == serialNumber)                 
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
