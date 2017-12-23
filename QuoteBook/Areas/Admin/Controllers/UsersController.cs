using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using QuoteBook.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuoteBook.Web.Areas.Admin.Models.Users;
using QuoteBook.Web.Infrastructure.Extentions;
using QuoteBook.Services.AdminService;
using QuoteBook.Constants;

namespace QuoteBook.Areas.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IAdminUserService users,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.users.All();

            return View(new AdminUserListingViewModel
            {
                Users = users
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, WebConstants.InvalidIdentityDetailsOrUser);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var role = model.Role;

            var success = await this.users.ChangeUserRoleAsync(user, role);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage(String.Format(WebConstants.TempDataUserRoleSuccessfullChange, user.Name, model.Role));

            return RedirectToAction(nameof(Index));
        }
    }
}