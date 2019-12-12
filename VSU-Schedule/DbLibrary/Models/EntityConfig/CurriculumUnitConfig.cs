using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbLibrary.Models.Entity;

namespace DbLibrary.Models.EntityConfig
{
    public class CurriculumUnitConfig : IEntityTypeConfiguration<CurriculumUnit>
    {
        public void Configure(EntityTypeBuilder<CurriculumUnit> builder)
        {
            builder.HasKey(cu => cu.Id);
            builder.HasAlternateKey(cu => new { cu.SemesterNumber, cu.SpecializationId });
            //builder.HasIndex(cu => new { cu.SemesterNumber, cu.SpecializationId });
            builder.Property(cu => cu.Id)
                .ValueGeneratedOnAdd();
            //builder.Property(cu => cu.QuantityLab)
            //    .HasDefaultValue(0);
            //builder.Property(cu => cu.QuantityLect)
            //    .HasDefaultValue(0);
            //builder.Property(cu => cu.QuantityPrac)
            //    .HasDefaultValue(0);
            builder.HasOne(cu => cu.Specialization)
                .WithMany(s => s.CurriculumUnits)
                .HasForeignKey(cu => cu.SpecializationId);
        }
    }
}
