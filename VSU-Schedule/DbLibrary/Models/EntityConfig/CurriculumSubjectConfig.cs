using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbLibrary.Models.Entity;

namespace DbLibrary.Models.EntityConfig
{
    public class CurriculumSubjectConfig : IEntityTypeConfiguration<CurriculumSubject>
    {
        public void Configure(EntityTypeBuilder<CurriculumSubject> builder)
        {
            builder.HasKey(cs => cs.Id);
            builder.HasAlternateKey(cs => new { cs.SubjectId, cs.CurriculumId });
            builder.Property(cs => cs.Id)
                .ValueGeneratedOnAdd();
            builder.HasOne(cs => cs.Subject)
                .WithMany(s => s.CurriculumSubjects)
                .HasForeignKey(cs => cs.SubjectId);
            builder.HasOne(cs => cs.CurriculumUnit)
                .WithMany(cu => cu.CurriculumSubjects)
                .HasForeignKey(cs => cs.CurriculumId);
        }
    }
}
