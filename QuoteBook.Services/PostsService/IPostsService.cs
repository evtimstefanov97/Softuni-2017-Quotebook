using QuoteBook.Data.Models;
using QuoteBook.Services.PostsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Services.PostsService
{
    public interface IPostsService
    {
        Task<IEnumerable<PostsListingModel>> AllByUser(string userId);

        Task<IEnumerable<PostsListingModel>> All();

        Task<IEnumerable<PostsListingModel>> AllOrdered(string column, string type);

        Task<PostEditDeleteModel> FindPostEditDeleteAsync(string postId);

        Task<Post> FindPostAsync(string postId);

        Task EditPostAsync(Category category,Inspirator inspirator,string quote,string postId, User author);

        Task<PostDetailsModel> FindPostDetailsAsync(string postId);

        Task CreatePostAsync(User author,Category category,Inspirator inspirator,string quote);

        Task DeletePostAsync(string postId);

        Task EditPostAsync(string postId);
    }
}
