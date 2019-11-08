using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbLibrary.Models.Entity;

namespace DbLibrary.Models.EntityConfig
{
    public class SubjConfig: IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd();
            builder.Property(s => s.Name)
                .HasMaxLength(60);
            builder.Ignore(s => s.ForTeacher);
            //builder.HasOne(g => g.Semester)
            //    .WithOne(g => g.Subject)
            //    .HasForeignKey<Subject>(g => g.SemesterNumber);
        }
    }
}
