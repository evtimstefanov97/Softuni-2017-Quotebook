using QuoteBook.Common.Mapping;
using QuoteBook.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace QuoteBook.Areas.Quote.Models
{
    public class AddPostViewModel:IMapFrom<Post>
    {
        [Required]
        [MaxLength(500, ErrorMessage = "The quote's length is a maximum of 500 characters")]
        public string Quote { get; set; }

        [Display(Name ="Inspirator")]
        public string InspiratorId { get; set; }

        [Display(Name ="Category")]
        public string CategoryId { get; set; }

    }
}
