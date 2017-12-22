using QuoteBook.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Services.PostsService.Models
{
    public class PostDetailsModel
    {
        public string Id { get; set; }
        public string Quote { get; set; }
        [Display(Name = "Category Title")]

        public string CategoryTitle { get; set; }
        [Display(Name ="Author")]
        public User Author { get; set; }

        [Display(Name = "Inspirator Name")]
        public string InspiratorName { get; set; }
        public byte[] InspiratorAvatar { get; set; }
    }
}
