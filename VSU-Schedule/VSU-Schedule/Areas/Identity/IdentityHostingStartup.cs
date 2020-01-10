using System;
using System.Threading.Tasks;
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
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<VSU_ScheduleUserContext>();
                
            });
        }

        private void AddToRoles(UserManager<VSU_ScheduleUser> userManager)
        {
            var adminUser = userManager.FindByNameAsync("Admin").Result;

            if (adminUser == null)
            {
                var administrator = new VSU_ScheduleUser()
                {
                    Email = "admin@admin.ru",
                    UserName = "Admin",
                };

                var newUser = userManager.CreateAsync(administrator, "P@ssword123").Result;
                if (newUser.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Admin");
                    newUserRole.Wait();
                }
            }
        }

    }
}