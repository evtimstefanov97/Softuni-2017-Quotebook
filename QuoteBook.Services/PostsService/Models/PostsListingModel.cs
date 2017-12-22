using QuoteBook.Common.Mapping;
using QuoteBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using QuoteBook.Services.LikesService.Models;

namespace QuoteBook.Services.PostsService.Models
{
    public class PostsListingModel:IMapFrom<Post>,IHaveCustomMapping
    {
        public string Id { get; set; }
        public string Quote { get; set; }
        public Category Category { get; set; }
        public User Author { get; set; }
        public Inspirator Inspirator { get; set; }
        public List<LikeViewModel> Likes { get; set; }
        public DateTime Created { get; set; }
        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Post, PostsListingModel>()
                 .ForMember(p => p.Likes, cfg => cfg.MapFrom(pl => pl.Likes));
        }
    }
}
