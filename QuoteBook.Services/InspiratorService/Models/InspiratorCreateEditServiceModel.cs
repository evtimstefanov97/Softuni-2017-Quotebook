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
        public DateTime BirthDate { get; set; }

    
        [Display(Name = "Inspirator picture")]
        public IFormFile Image { get; set; }

    }
}
