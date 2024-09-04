using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polyclinic.TestTask.API.Models.Entities;

namespace Polyclinic.TestTask.API.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация сущности участок.
    /// </summary>
    public class MedicalDistrictConfiguration : IEntityTypeConfiguration<MedicalDistrict>
    {
        public void Configure(EntityTypeBuilder<MedicalDistrict> builder)
        {
            builder.ToTable("Districts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
