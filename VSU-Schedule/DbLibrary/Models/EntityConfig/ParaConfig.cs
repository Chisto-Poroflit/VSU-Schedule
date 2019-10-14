using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbLibrary.Models.Entity;

namespace DbLibrary.Models.EntityConfig
{
    public class ParaConfig : IEntityTypeConfiguration<Para>
    {
        public void Configure(EntityTypeBuilder<Para> builder)
        {
            builder.HasKey(p => p.Number);
            builder.Property(dt => dt.StartTime).HasMaxLength(40);
            builder.Property(dt => dt.EndTime).HasMaxLength(40);
            builder.HasData(
                new Para[]
                {
                    new Para{Number = 1, StartTime = "8:00", EndTime = "9:35" },

                    new Para{Number = 2,StartTime = "9:45", EndTime = "11:20" },

                    new Para{Number = 3,StartTime = "11:30", EndTime = "13:05" },

                    new Para{Number = 4,StartTime = "13:25", EndTime = "15:00" },

                    new Para{Number = 5,StartTime = "15:10", EndTime = "16:45" },

                    new Para{Number = 6,StartTime = "16:55", EndTime = "18:30" },

                    new Para{Number = 7,StartTime = "18:40", EndTime = "20:00" },

                    new Para{Number = 8, StartTime = "20:10", EndTime = "21:30"}
                });
        }
    }
}
