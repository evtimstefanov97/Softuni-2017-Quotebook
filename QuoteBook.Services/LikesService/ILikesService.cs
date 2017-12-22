using QuoteBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Services.LikesService
{
    public interface ILikesService
    {
        Task Like(string PostId, User user);
    }
}
