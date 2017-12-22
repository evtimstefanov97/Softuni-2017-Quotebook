using QuoteBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Services.PostsService.Models
{
    public class PostEditDeleteModel
    {
        public string Id { get; set; }
        public string Quote { get; set; }
        public User Author { get; set; }
        public Category Category { get; set; }
        public Inspirator Inspirator { get; set; }
    }
}
