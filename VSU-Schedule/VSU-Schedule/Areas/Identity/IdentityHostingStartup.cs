using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VSU_Schedule.Areas.Identity.Data;
using VSU_Schedule;

[assembly: HostingStartup(typeof(VSU_Schedule.Areas.Identity.IdentityHostingStartup))]
namespace VSU_Schedule.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<VSU_ScheduleUserContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("VSU_ScheduleUserContextConnection")));

                services.AddDefaultIdentity<VSU_ScheduleUser>()
                    .AddDefaultUI(UIFramework.Bootstrap4)
                    .AddEntityFrameworkStores<VSU_ScheduleUserContext>();
            });
        }
    }
}