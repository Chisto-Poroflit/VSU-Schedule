using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbLibrary.Models.Entity;

namespace DbLibrary.Models.EntityConfig
{
    public class RoomConfig: IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Number);
            builder.Property(r => r.Number)
                .ValueGeneratedNever();
            builder.Property(r => r.Number).HasMaxLength(40);
        }
    }
}
