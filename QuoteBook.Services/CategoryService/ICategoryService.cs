using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuoteBook.Data.Models;
using QuoteBook.Services.CategoryService.Models;

namespace QuoteBook.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListingModel>> All();

        Task<Category> FindCategoryAsync(string categoryId);
    }
}
