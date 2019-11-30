using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbLibrary.Models.Entity;

namespace DbLibrary.Models.EntityConfig
{
    public class CoupleGroupConfig : IEntityTypeConfiguration<CoupleGroup>
    {
        public void Configure(EntityTypeBuilder<CoupleGroup> builder)
        {
            builder.HasKey(cg => new { cg.GroupId, cg.CoupleId });
            builder.HasOne(cg => cg.Couple)
                .WithMany(c => c.CoupleGroups)
                .HasForeignKey(cg => cg.CoupleId);
            builder.HasOne(cg => cg.Group)
                .WithMany(g => g.CoupleGroups)
                .HasForeignKey(cg => cg.GroupId);
        }
    }
}
