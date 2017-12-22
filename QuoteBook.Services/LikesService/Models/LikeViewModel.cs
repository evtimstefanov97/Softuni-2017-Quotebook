using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using QuoteBook.Data.Models;

using QuoteBook.Common.Mapping;

namespace QuoteBook.Services.LikesService.Models
{
    public class LikeViewModel : IMapFrom<Like>
    {
        public string PostId { get; set; }

        public string UserId { get; set; }
    }
}
