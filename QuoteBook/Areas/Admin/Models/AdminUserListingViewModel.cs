using Microsoft.AspNetCore.Mvc.Rendering;
using QuoteBook.Services.AdminService.Models;
using System.Collections.Generic;


namespace QuoteBook.Web.Areas.Admin.Models.Users
{
    public class AdminUserListingViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

    }
}
