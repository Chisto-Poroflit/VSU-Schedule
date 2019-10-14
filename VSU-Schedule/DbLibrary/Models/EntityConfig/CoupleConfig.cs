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
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();
            builder.Property(c => c.Day)
                .IsRequired();
            //    .HasComputedColumnSql("CHECK(Day >= 0 AND Day <= 6)");
            builder.Property(c => c.LessonType)
                .IsRequired();
            //    .HasComputedColumnSql("CHECK(LessonType >= 0 AND LessonType <= 2)");
            builder.Property(c => c.Numerator)
                .IsRequired();
            builder.Property(c => c.Denomirator)
                .IsRequired();
              //  .HasComputedColumnSql("CHECK((Numerator = 0 AND Denomirator = 1) OR (Numerator = 1 AND Denomirator = 0) OR (Numerator = 1 AND Denomirator = 1))");
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
