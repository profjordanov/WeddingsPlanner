using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WeddingsPlanner.Data.Entities;
using WeddingsPlanner.Data.EntityFramework;
using static WeddingsPlanner.Api.GlobalConstants;

namespace WeddingsPlanner.Api.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    // Seed Roles
                    var roles = new[]
                    {
                        AdministratorRoleName
                    };

                    foreach (var role in roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role);

                        if (roleExists)
                        {
                            continue;
                        }

                        var result = await roleManager.CreateAsync(new IdentityRole(role));
                        if (!result.Succeeded)
                        {
                            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                        }
                    }

                    // Seed Admin User
                    var adminUser = await userManager.FindByEmailAsync(AdminEmail);
                    if (adminUser == null)
                    {
                        // Create Admin User
                        adminUser = new User
                        {
                            UserName = AdminUsername,
                            Email = AdminEmail,
                            PhoneNumber = "359878000000"
                        };

                        var result = await userManager.CreateAsync(adminUser, AdminPassword);

                        // Add User to Role
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(adminUser, AdministratorRoleName);
                            await userManager.AddClaimAsync(adminUser,
                                new Claim(ClaimTypes.Role, AdministratorRoleName));
                        }
                        else
                        {
                            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                        }
                    }
                })
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}