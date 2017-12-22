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
    public class InspiratorListingModel :IMapFrom<Inspirator>,IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name="Picture")]
        public byte[] Image { get; set; }
        [Display(Name="Number of times quoted")]
        public int TimesQuoted { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Inspirator, InspiratorListingModel>()
                .ForMember(i => i.Image, cfg => cfg.MapFrom(ins => ins.Image));
            mapper.CreateMap<Inspirator, InspiratorListingModel>()
                .ForMember(i => i.TimesQuoted, cfg => cfg.MapFrom(ins => ins.Posts.Count()));
        }
    }
}
