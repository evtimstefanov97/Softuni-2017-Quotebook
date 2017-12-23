using System;

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using QuoteBook.Data;
using QuoteBook.Data.Models;
using QuoteBook.Constants;

namespace QuoteBook.Web.Infrastructure.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<QuoteBookDbContext>()
                .Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                {
                    var adminRole = WebConstants.AdministratorRole;

                    var roles = new[]
                    {
                        adminRole,
                        WebConstants.AuthorRole,
                        WebConstants.ViewerRole
                    };

                    foreach (var role in roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role);

                        if (!roleExists)
                        {
                            var result = await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = role
                            });
                        }
                    }
                    var adminEmail = "admin@admin.com";

                    var adminUser = await userManager.FindByEmailAsync(adminEmail);

                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            Email = adminEmail,
                            UserName = adminEmail,
                            Name = adminRole,
                            Birthdate = DateTime.UtcNow
                        };

                        await userManager.CreateAsync(adminUser, "admin1234");

                        await userManager.AddToRoleAsync(adminUser, adminRole);
                    }
                }).Wait();
            }
            return app;
        }
    }
}
