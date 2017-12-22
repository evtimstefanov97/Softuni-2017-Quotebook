using Microsoft.AspNetCore.Http;
using QuoteBook.Common.Mapping;
using QuoteBook.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace QuoteBook.Services.InspiratorService.Models
{
    public class InspiratorCreateEditServiceModel:IMapFrom<Inspirator>
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name="Born")]
        [Required]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2017", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime BirthDate { get; set; }

    
        [Display(Name = "Inspirator picture")]
        public IFormFile Image { get; set; }

    }
}
