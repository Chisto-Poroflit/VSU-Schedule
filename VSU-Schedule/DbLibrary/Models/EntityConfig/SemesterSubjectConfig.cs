using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbLibrary.Models.Entity;

namespace DbLibrary.Models.EntityConfig
{
    public class SemesterSubjectConfig: IEntityTypeConfiguration<SemesterSubject>
    {
        public void Configure(EntityTypeBuilder<SemesterSubject> builder)
        {
            builder.HasKey(ss => new { ss.SemesterId, ss.SubjectId });
            builder.HasOne(ss => ss.Semester)
                .WithMany(s => s.SemesterSubjects)
                .HasForeignKey(ss => ss.SemesterId);
            builder.HasOne(ss => ss.Subject)
                .WithMany(s => s.SemesterSubjects)
                .HasForeignKey(ss => ss.SubjectId);
        }
    }
}
