using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuoteBook.Data;
using QuoteBook.Models;
using QuoteBook.Services;
using QuoteBook.Data.Models;
using QuoteBook.Web.Infrastructure.Extentions;
using QuoteBook.Services.CategoryService;
using QuoteBook.Services.CategoryService.Implementations;
using QuoteBook.Services.InspiratorService;
using QuoteBook.Services.InspiratorService.Implementations;
using QuoteBook.Services.PostsService;
using QuoteBook.Services.PostsService.Implementations;
using AutoMapper;
using QuoteBook.Services.LikesService;
using QuoteBook.Services.LikesService.Implementations;
using QuoteBook.Services.AdminService;
using QuoteBook.Services.AdminService.Implementations;

namespace QuoteBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QuoteBookDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<DataSeeder>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IInspiratorService, InspiratorService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<ILikesService, LikesService>();
            services.AddTransient<IAdminUserService, AdminUserService>();
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<QuoteBookDbContext>()
                .AddDefaultTokenProviders();
            services.AddAutoMapper();
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authenticator:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataSeeder seeder)
        {
            app.UseDatabaseMigration();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "profile",
                   template: "users/{username}",
                   defaults: new { controller = "Users", action = "Profile" });

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            seeder.Seed().Wait();
        }
    }
}
