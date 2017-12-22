using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Data.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Quote { get; set; }

        public DateTime Created { get; set; }

        public Category Category { get; set; }
        public string CategoryId { get; set; }

        public User Author { get; set; }
        public string AuthorId { get; set; }

        public Inspirator Inspirator { get; set; }
        public string InspiratorId { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
