
namespace QuoteBook.Test.Services
{
    using FluentAssertions;
    using System.Threading.Tasks;
    using Xunit;
    using QuoteBook.Services.AdminService.Implementations;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using QuoteBook.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features.Authentication;
    using QuoteBook.Constants;
    using AutoMapper;
    using QuoteBook.Web.Infrastructure.Mapping;

    public class DatabaseSetupTests
    {
        public QuoteBookDbContext Context { get; set; }

        public UserManager<User> UserManager { get; }

        public RoleManager<IdentityRole> RoleManager { get; }

        public DatabaseSetupTests()
        {
            var services = new ServiceCollection();
            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<QuoteBookDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<QuoteBookDbContext>();
            var context = new DefaultHttpContext();
            context.Features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature());
            services.AddSingleton<IHttpContextAccessor>(h => new HttpContextAccessor { HttpContext = context });
            var serviceProvider = services.BuildServiceProvider();

            Context = serviceProvider.GetRequiredService<QuoteBookDbContext>();
            UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            this.AddRoles();
        }
        private void AddRoles()
        {
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
                    var roleExists = await RoleManager.RoleExistsAsync(role);

                    if (!roleExists)
                    {
                        var result = await RoleManager.CreateAsync(new IdentityRole
                        {
                            Name = role
                        });
                    }
                }
            }).Wait();
        }

        [Fact]
        public async Task ChangeUserRoleShouldChangeRole()
        {
            //Arrange

            var adminService = new AdminUserService(Context, UserManager);

            User user = new User()
            {
                Name = "User",
                Email="user@abv.bg",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            await UserManager.AddToRoleAsync(user, WebConstants.AdministratorRole);

            await Context.AddAsync(user);
            await Context.SaveChangesAsync();
           
            //Act

            var newUser = await Context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);
            newUser.UserName = "evtim@abv.bg";

            var roleChange = WebConstants.AuthorRole;
            var result=await adminService.ChangeUserRoleAsync(newUser, roleChange);

            //Assert
            var userRole = await UserManager.IsInRoleAsync(newUser, roleChange);
            var userRoles = await UserManager.GetRolesAsync(newUser);

            userRole.Should().Be(true);
            userRoles.Count.Should().Be(1);
        }

        [Fact]
        public async Task FindUsersAsyncShouldReturnAllExceptAdmin()
        {
            //Arrange
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());

            var adminService = new AdminUserService(Context, UserManager);

            User admin = new User()
            {
                Name = "Admin",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            await UserManager.AddToRoleAsync(admin, "Administrator");

            var users = new List<User>()
            {
                new User()
                {
                    Name="User1",
                       UserName="Username1",
                    SecurityStamp=Guid.NewGuid().ToString()

                },
                new User()
                {
                    Name="User2",
                       UserName="Username1",
                    SecurityStamp=Guid.NewGuid().ToString()

                },
                new User()
                {
                    Name="User3",
                       UserName="Username3",
                    SecurityStamp=Guid.NewGuid().ToString()
                }
            };

            foreach (var user in users)
            {
                await UserManager.AddToRoleAsync(user, "Author");
            }

            await Context.AddRangeAsync(users);
            await Context.AddAsync(admin);
            await Context.SaveChangesAsync();

            //Act

            var result = await adminService.All();  
            
            //Assert

            result
                .Should()
                .NotContain(u => u.Name == "Admin");

            result
                .Should()
                .Contain(u => u.Name == "User2" 
                || u.Name=="User1" 
                || u.Name=="User3");
        }
    }

}
