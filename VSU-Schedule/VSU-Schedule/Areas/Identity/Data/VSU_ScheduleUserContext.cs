using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VSU_Schedule.Areas.Identity.Data;
using VSU_Schedule.Areas.Identity.Data.Config;

namespace VSU_Schedule
{
    public class VSU_ScheduleUserContext : IdentityDbContext<VSU_ScheduleUser>
    {
        public VSU_ScheduleUserContext(DbContextOptions<VSU_ScheduleUserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RolesConfig());
        }
    }
}
