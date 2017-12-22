using QuoteBook.Common.Mapping;
using QuoteBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Services.CategoryService.Models
{
    public class CategoryListingModel:IMapFrom<Category>
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
}
