using drones.API.Data;
using drones.API.Models;

namespace drones.API.Repositories
{
    public interface IPeriodicTaskLogRepository : IBaseRepository<PeriodicTaskLog>
    {
        
    }

    public class PeriodicTaskLogRepository : BaseRepository<PeriodicTaskLog>, IPeriodicTaskLogRepository
    {
        public PeriodicTaskLogRepository(DroneApiDbContext context) : base(context) { }       
    }    
}
