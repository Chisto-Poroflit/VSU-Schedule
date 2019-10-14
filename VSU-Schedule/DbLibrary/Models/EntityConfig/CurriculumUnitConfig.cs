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
            builder.Property(cu => cu.Id)
                .ValueGeneratedOnAdd();
            builder.Property(cu => cu.QuantityLab)
                .HasDefaultValue(0);
            builder.Property(cu => cu.QuantityLect)
                .HasDefaultValue(0);
            builder.Property(cu => cu.QuantityPrac)
                .HasDefaultValue(0);
            builder.HasOne(cu => cu.Specialization)
                .WithOne(s => s.CurriculumUnit)
                .HasForeignKey<CurriculumUnit>(cu => cu.SpecializationId);
            builder.HasOne(cu => cu.Subject)
                .WithOne(s => s.CurriculumUnit)
                .HasForeignKey<CurriculumUnit>(cu => cu.SubjectId);
        }
    }
}
