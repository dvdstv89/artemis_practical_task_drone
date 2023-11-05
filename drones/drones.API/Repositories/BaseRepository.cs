using drones.API.Data;
using drones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace drones.API.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DroneApiDbContext _context;
        protected DbSet<T> dbSet;

        public BaseRepository(DroneApiDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {               
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
