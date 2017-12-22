using QuoteBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Data
{
    public class DataSeeder
    {
        private QuoteBookDbContext _context;

        public DataSeeder(QuoteBookDbContext context)
        {
            _context = context;
        }



        public async Task Seed()
        {
            if (!_context.Categories.Any())
            {
                _context.AddRange(categories);
                await _context.SaveChangesAsync();
            }
        }
        List<Category> categories = new List<Category>
        {
            new Category()
            {
                Title="Sports",
                Posts=new List<Post>()
            },
            new Category()
            {
                Title="Science",
                Posts=new List<Post>()
            },
            new Category()
            {
                Title="Literature",
                Posts=new List<Post>()
            },           
              new Category()
            {
                Title="Life",
                Posts=new List<Post>()
            },
                new Category()
            {
                Title="Software",
                Posts=new List<Post>()
            },
                  new Category()
            {
                Title="Fashion",
                Posts=new List<Post>()
            },
                    new Category()
            {
                Title="History",
                Posts=new List<Post>()
            },
        };
    }
}
