using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuoteBook.Services.AdminService.Models;
using QuoteBook.Data;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace QuoteBook.Services.AdminService.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly QuoteBookDbContext db;

        public AdminUserService(QuoteBookDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> All()
        => await this.db
            .Users
            .ProjectTo<AdminUserListingServiceModel>()
            .ToListAsync();
    }
}
