using Microsoft.EntityFrameworkCore;
using Polyclinic.TestTask.API.Models.Entities;

namespace Polyclinic.TestTask.API.DataAccess
{
    /// <summary>
    /// Контекст базы данных поликлиники.
    /// </summary>
    public class PolyclinicDbContext(IConfiguration configuration) : DbContext
    {
        #region db сеты
        /// <summary>
        /// Таблица с пациентами.
        /// </summary>
        public DbSet<Patient> Patients => Set<Patient>();

        /// <summary>
        /// Таблица с врачами.
        /// </summary>
        public DbSet<Doctor> Doctors => Set<Doctor>();

        /// <summary>
        /// Таблица с участками.
        /// </summary>
        public DbSet<MedicalDistrict> MedicalDistricts => Set<MedicalDistrict>();

        /// <summary>
        /// Таблица со специализациями.
        /// </summary>
        public DbSet<Specialization> Specializations => Set<Specialization>();

        /// <summary>
        /// Таблица с кабинетами.
        /// </summary>
        public DbSet<Cabinet> Cabinets => Set<Cabinet>(); 
        #endregion

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
