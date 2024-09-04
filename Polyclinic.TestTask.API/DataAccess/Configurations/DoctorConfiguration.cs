using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polyclinic.TestTask.API.Models.Entities;

namespace Polyclinic.TestTask.API.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация сущности врача.
    /// </summary>
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.FIO)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasOne(p => p.Cabinet).WithMany();
            builder.HasOne(p => p.Specialization).WithMany();
            builder.HasOne(p => p.MedicalDistrict).WithMany();
        }
    }
}
