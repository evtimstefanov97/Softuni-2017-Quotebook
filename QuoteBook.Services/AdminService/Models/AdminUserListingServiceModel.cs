using QuoteBook.Common.Mapping;
using QuoteBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Services.AdminService.Models
{
    public class AdminUserListingServiceModel:IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
