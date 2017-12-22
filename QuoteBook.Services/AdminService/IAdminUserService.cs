using QuoteBook.Services.AdminService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Services.AdminService
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> All();
    }
}
