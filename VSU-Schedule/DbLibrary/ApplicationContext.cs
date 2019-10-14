using Microsoft.EntityFrameworkCore;
using DbLibrary.Models.EntityConfig;
using DbLibrary.Models.Entity;

namespace DbLibrary
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<CurriculumUnit> CurriculumUnits { get; set; }
        public DbSet<Couple> Couples { get; set; }
        public DbSet<CoupleGroup> CoupleGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DimasGotovsya;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ParaConfig());
            modelBuilder.ApplyConfiguration(new SubjConfig());
            modelBuilder.ApplyConfiguration(new RoomConfig());
            modelBuilder.ApplyConfiguration(new TeacherConfig());
            modelBuilder.ApplyConfiguration(new SpecializationConfig());
            modelBuilder.ApplyConfiguration(new CurriculumUnitConfig());
            modelBuilder.ApplyConfiguration(new GroupConfig());
            modelBuilder.ApplyConfiguration(new CoupleConfig());
            modelBuilder.ApplyConfiguration(new CoupleGroupConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
