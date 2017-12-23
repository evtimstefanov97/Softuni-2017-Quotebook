using QuoteBook.Common.Mapping;
using QuoteBook.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuoteBook.Constants;

namespace QuoteBook.Areas.Quote.Models
{
    public class AddPostViewModel:IMapFrom<Post>
    {
        [Required]
        [MaxLength(500, ErrorMessage = WebConstants.QuoteMaxLength)]
        public string Quote { get; set; }

        [Display(Name ="Inspirator")]
        public string InspiratorId { get; set; }

        [Display(Name ="Category")]
        public string CategoryId { get; set; }

    }
}
