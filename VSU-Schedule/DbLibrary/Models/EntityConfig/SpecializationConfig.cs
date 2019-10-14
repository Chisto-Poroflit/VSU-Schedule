using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbLibrary.Models.Entity;

namespace DbLibrary.Models.EntityConfig
{
    public class SpecializationConfig : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd();
            builder.Property(s => s.Name)
                .HasMaxLength(500);
            builder.HasOne(s => s.ParentSpec)
                .WithMany(s => s.ChildSpecializations)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(
                new Specialization[]
                {
                    new Specialization {Id = 1, Name = "Информационные системы и технологии"},
                    new Specialization {Id = 2, Name = "Математика и компьютерные науки"},
                    new Specialization {Id = 3, Name = "Программная инженерия"},
                    new Specialization {Id = 4, Name = "Прикладная информатика"},
                    new Specialization {Id = 5, Name = "Информационная безопасность"},
                    new Specialization {Id = 6, Name = "Компьютерая безопасность"},

                    new Specialization {Id = 7, Name = "Информационные системы"},//1
                    new Specialization {Id = 8, Name = "Программирования и информационных технологий"},//3
                    new Specialization {Id = 9, Name = "Информационных технологий управления"},//1
                    new Specialization {Id = 10, Name = "Информационные системы в телекоммуникациях"},//1
                    new Specialization {Id = 11, Name = "Программная инженерия в информационных системах"},//1
                    new Specialization {Id = 12, Name = "Теоретические основы защиты информации"},//1
                    new Specialization {Id = 13, Name = "Цифровых технологий"}//1
                });
        }
    }
}
