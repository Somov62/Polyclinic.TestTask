using Microsoft.EntityFrameworkCore;
using Polyclinic.TestTask.API.Models.Entities;

namespace Polyclinic.TestTask.API.DataAccess
{
    /// <summary>
    /// Контекст базы данных поликлиники.
    /// </summary>
    public class PolyclinicDbContext(IConfiguration configuration) : DbContext
    {
        /// <summary>
        /// Таблица с пациентами.
        /// </summary>
        public DbSet<Patient> Patients => Set<Patient>();

        /// <summary>
        /// Таблица с врачами.
        /// </summary>
        public DbSet<Doctor> Doctors => Set<Doctor>();

        /// <summary>
        /// Конфигурация правил работы.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString(nameof(PolyclinicDbContext)));
        }

        /// <summary>
        /// Конфигурация сущностей.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PolyclinicDbContext).Assembly);
        }
    }
}
