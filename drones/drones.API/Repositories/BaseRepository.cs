﻿using drones.API.Data;
using Microsoft.EntityFrameworkCore;

namespace drones.API.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
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
                dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
