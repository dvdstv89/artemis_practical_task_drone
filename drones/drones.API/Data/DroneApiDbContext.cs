using drones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace drones.API.Data
{
    public class DroneApiDbContext : DbContext
    {
        public DroneApiDbContext(DbContextOptions<DroneApiDbContext> options) : base(options)
        {
            
        }
        public DbSet<Drone> Drones { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<DroneMedication> DroneMedication { get; set; }
        public DbSet<PeriodicTaskLog> PeriodicTaskLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DroneMedication>().Property(e => e.Id).ValueGeneratedNever();           

            modelBuilder.Entity<Drone>()
                .HasMany(e => e.DroneMedications)
                .WithOne()
                .HasForeignKey(e => e.DroneSerialNumber)
                .IsRequired();

            modelBuilder.Entity<Medication>()
                .HasMany(e => e.DroneMedications)
                .WithOne()
                .HasForeignKey(e => e.MedicationCode)
                .IsRequired();
        }
    }
}
