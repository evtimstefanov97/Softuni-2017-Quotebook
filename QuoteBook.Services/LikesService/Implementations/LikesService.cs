using Microsoft.EntityFrameworkCore;
using QuoteBook.Data;
using QuoteBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Services.LikesService.Implementations
{
    public class LikesService : ILikesService
    {
        private readonly QuoteBookDbContext context;
        public LikesService(QuoteBookDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Like(string PostId, User user)
        {

            var post = await this.context.Posts.Include(p => p.Likes).FirstOrDefaultAsync(p => p.Id == PostId);

            if (post == null || user == null)
            {
                return false;
            }
            if (post.Likes.Any(l => l.User == user))
            {
                post.Likes.Remove(post.Likes.Where(l => l.User == user).FirstOrDefault());
                await context.SaveChangesAsync();

                return true;
            }

            var like = new Like()
            {
                Post = post,
                User = user
            };

            await context.Likes.AddAsync(like);
            post.Likes.Add(like);
            context.SaveChanges();

            return true;
        }
    }
}
