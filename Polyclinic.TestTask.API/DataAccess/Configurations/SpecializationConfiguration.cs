using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polyclinic.TestTask.API.Models.Entities;

namespace Polyclinic.TestTask.API.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация сущности специализация.
    /// </summary>
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.ToTable("Specializations");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
