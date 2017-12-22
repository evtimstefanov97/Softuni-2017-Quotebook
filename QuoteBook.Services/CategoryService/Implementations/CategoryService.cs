using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuoteBook.Data;
using QuoteBook.Data.Models;
using QuoteBook.Services.CategoryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Services.CategoryService.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly QuoteBookDbContext context;
        public CategoryService(QuoteBookDbContext context)
        {
            this.context = context;
        }

        public Task CreateCategoryAsync(string Title)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(string categoryId)
        {
            throw new NotImplementedException();
        }

        public Task EditCategory(string categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> FindCategoryAsync(string categoryId)
        {
            return await context.Categories.FindAsync(categoryId);
        }

        public async Task<IEnumerable<CategoryListingModel>> All()
        {
            return await this.context.Categories.ProjectTo<CategoryListingModel>().ToListAsync();
        }

    }
}
