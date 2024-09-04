using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polyclinic.TestTask.API.Models.Entities;

namespace Polyclinic.TestTask.API.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация сущности пациент.
    /// </summary>
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Patronymic)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Address)
                .IsRequired();

            builder.Property(p => p.BirthDate)
                .IsRequired();

            builder.HasOne(p => p.MedicalDistrict).WithMany();
        }
    }
}
