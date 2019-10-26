using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbLibrary.Models.Entity;

namespace DbLibrary.Models.EntityConfig
{
    public class TeacherSubjectConfig: IEntityTypeConfiguration<TeacherSubject>
    {
        public void Configure(EntityTypeBuilder<TeacherSubject> builder)
        {
            builder.HasKey(ts => new { ts.SubjectId, ts.TeacherId});
            builder.HasOne(ts => ts.Teacher)
                .WithMany(t => t.TeacherSubjects)
                .HasForeignKey(ts => ts.TeacherId);
            builder.HasOne(ts => ts.Subject)
                .WithMany(t => t.TeacherSubjects)
                .HasForeignKey(ts => ts.SubjectId);
        }
    }
}
