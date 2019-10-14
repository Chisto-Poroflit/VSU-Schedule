using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbLibrary.Models.Entity;

namespace DbLibrary.Models.EntityConfig
{
    public class GroupConfig : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id)
                .ValueGeneratedOnAdd();
            builder.Property(g => g.SemesterNumber).IsRequired();
            builder.Property(g => g.SubgroupNumber).IsRequired();
            builder.Property(g => g.GroupNumber).IsRequired();
            builder.HasOne(g => g.Specialization)
                .WithMany(s => s.Group)
                .HasForeignKey(g => g.SpecializatioId);
        }
    }
}
