using Microsoft.EntityFrameworkCore;
using Polyclinic.TestTask.API.Models.Entities;

namespace Polyclinic.TestTask.API.DataAccess
{
    /// <summary>
    /// Контекст базы данных поликлиники.
    /// </summary>
    public class PolyclinicDbContext : DbContext
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
        /// Конфигурация сущностей.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PolyclinicDbContext).Assembly);
        }
    }
}
