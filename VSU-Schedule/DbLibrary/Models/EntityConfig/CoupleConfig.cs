using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbLibrary.Models.Entity;

namespace DbLibrary.Models.EntityConfig
{
    public class CoupleConfig : IEntityTypeConfiguration<Couple>
    {
        public void Configure(EntityTypeBuilder<Couple> builder)
        {
            builder.HasKey(c => c.Id);
            //builder.HasAlternateKey(c => new { c.Day, c.ParaId, c.RoomId, c.Denomirator, c.Numerator });
            //builder.HasAlternateKey(c => new { c.Day, c.ParaId, c.TeacherId, c.Numerator, c.Denomirator, c.CoupleGroups });
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();
            builder.Property(c => c.Day)
                .IsRequired();
            builder.Property(c => c.LessonType)
                .IsRequired();
            builder.Property(c => c.Numerator)
                .IsRequired();
            builder.Property(c => c.Denomirator)
                .IsRequired();
            builder.HasOne(c => c.Para)
                .WithMany(dt => dt.Couples)
                .HasForeignKey(c => c.ParaId);
            builder.HasOne(c => c.Room)
                .WithMany(r => r.Couples)
                .HasForeignKey(c => c.RoomId);
            builder.HasOne(c => c.Subject)
                .WithMany(s => s.Couples)
                .HasForeignKey(c => c.SubjectId);
            builder.HasOne(c => c.Teacher)
                .WithMany(t => t.Couples)
                .HasForeignKey(c => c.TeacherId);
        }
    }
}
