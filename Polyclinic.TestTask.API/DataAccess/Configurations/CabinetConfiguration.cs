using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polyclinic.TestTask.API.Models.Entities;

namespace Polyclinic.TestTask.API.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация сущности кабинет.
    /// </summary>
    public class CabinetConfiguration : IEntityTypeConfiguration<Cabinet>
    {
        public void Configure(EntityTypeBuilder<Cabinet> builder)
        {
            builder.ToTable("Cabinets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
