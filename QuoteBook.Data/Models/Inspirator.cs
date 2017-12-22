using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Data.Models
{
    public class Inspirator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        public ICollection<Post> Posts { get; set; }
        [Required]
        public byte[] Image { get; set; }
    }
}
