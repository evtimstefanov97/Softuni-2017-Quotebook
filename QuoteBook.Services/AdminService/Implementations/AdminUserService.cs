using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuoteBook.Services.AdminService.Models;
using QuoteBook.Data;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using QuoteBook.Data.Models;
using Z.Linq;
namespace QuoteBook.Services.AdminService.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly QuoteBookDbContext db;
        private readonly UserManager<User> userManager;
        public AdminUserService(QuoteBookDbContext db,UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> All()
        {
            var allUsers = await this.db.Users.AsQueryable().ToListAsync();
            var allUsersExceptAdmin = new List<AdminUserListingServiceModel>();
            foreach(var user in allUsers)
            {
                var isUserAdmin = await this.userManager.IsInRoleAsync(user, "Administrator");
                if (!isUserAdmin)
                {
                    AdminUserListingServiceModel model = AutoMapper.Mapper.Map<AdminUserListingServiceModel>(user);

                    allUsersExceptAdmin.Add(model);
                }
            }

            return allUsersExceptAdmin;
        }

        public async Task<bool> ChangeUserRoleAsync(User user,string role)
        {
            var roles = await this.userManager.GetRolesAsync(user);

            var removeResult=await this.userManager.RemoveFromRolesAsync(user, roles);
            var addResult=await this.userManager.AddToRoleAsync(user, role);

            if(!addResult.Succeeded || !removeResult.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
