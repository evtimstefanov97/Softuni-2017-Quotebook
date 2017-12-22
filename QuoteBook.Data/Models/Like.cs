using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Data.Models
{
    public class Like
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        public Post Post { get; set; }
        public string PostId { get; set; }

    }

}
