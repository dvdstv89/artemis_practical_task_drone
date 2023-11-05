using drones.API.Data;
using drones.API.Models;

namespace drones.API.Repositories
{
    public interface IDroneRepository : IBaseRepository<Drone>
    {
       
    }

    public class DroneRepository : BaseRepository<Drone>, IDroneRepository
    {
        public DroneRepository(DroneApiDbContext context) : base(context) { }
    }
}
