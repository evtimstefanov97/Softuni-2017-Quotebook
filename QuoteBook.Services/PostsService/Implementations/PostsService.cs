using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuoteBook.Data.Models;
using QuoteBook.Services.PostsService.Models;
using QuoteBook.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System.Reflection;
using System.Linq.Expressions;
using System.Security.Claims;

namespace QuoteBook.Services.PostsService.Implementations
{
    public class PostsService : IPostsService
    {
        private readonly QuoteBookDbContext context;
        public PostsService(QuoteBookDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PostsListingModel>> AllByUser(string userId)
        {
            return await this.context.Posts.Where(p => p.Author.Id == userId).ProjectTo<PostsListingModel>().ToListAsync();
        }
        public async Task<IEnumerable<PostsListingModel>> All()
        {
            return await this.context.Posts.ProjectTo<PostsListingModel>().ToListAsync();
        }
        public async Task<IEnumerable<PostsListingModel>> AllOrdered(string column, string type)
        {
            Expression<Func<Post, object>> orderExpression;
            Expression<Func<Post, bool>> sortExpression = (x => x.Category.Title == type);


            switch (column)
            {
                case "Date":
                    orderExpression = (x => x.Created);
                    break;
                case "Name":
                    orderExpression = (x => x.Author.Name);
                    break;
                case "Likes":
                    orderExpression = (x => x.Likes.Count());
                    break;
                case "Category":
                    orderExpression = (x => x.Category.Title);

                    break;
                default:
                    orderExpression = (x => x.Created);
                    break;
            }


            if (type == "Asc")
            {
                var ordered = this.context.Posts.Include(p => p.Likes).OrderBy(orderExpression);
                return await ordered.ProjectTo<PostsListingModel>().ToListAsync();

            }
            else if (type == "Desc")
            {
                return await this.context.Posts.Include(p => p.Likes).OrderByDescending(orderExpression).ProjectTo<PostsListingModel>().ToListAsync();
            }

            else
            {

                return await this.context.Posts.Include(p => p.Likes).Where(sortExpression).ProjectTo<PostsListingModel>().ToListAsync();

            }
        }
        public async Task CreatePostAsync(User author, Category category, Inspirator inspirator, string quote)
        {
            Post post = new Post();
            post.Author = author;
            post.Category = category;
            post.Inspirator = inspirator;
            post.Quote = quote;
            post.Likes = new List<Like>();
            post.Created = DateTime.UtcNow;

            await this.context.Posts.AddAsync(post);
            await this.context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(string postId)
        {
            var post = await this.context.Posts.FindAsync(postId);
            this.context.Posts.Remove(post);
            await this.context.SaveChangesAsync();
        }

        public async Task EditPostAsync(string postId)
        {
            var post = await this.context.Posts.FindAsync(postId);
            this.context.Posts.Remove(post);
            await this.context.SaveChangesAsync();
        }
        public async Task EditPostAsync(Category category, Inspirator inspirator, string quote, string postId, User author)
        {
            var post = await this.context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            post.Author = author;
            post.Category = category;
            post.Quote = quote;
            post.Inspirator = inspirator;
            await context.SaveChangesAsync();
        }
        public async Task<PostDetailsModel> FindPostDetailsAsync(string postId)
        {
            var post = await this.context.Posts.Include(p => p.Category).Include(p => p.Inspirator).Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == postId);

            return new PostDetailsModel()
            {
                Id = post.Id,
                Quote = post.Quote,
                Author = post.Author,
                CategoryTitle = post.Category.Title,
                InspiratorName = post.Inspirator.Name,
                InspiratorAvatar = post.Inspirator.Image
            };
        }
        public async Task<Post> FindPostAsync(string postId)
        {
            var post = await this.context.Posts.Include(p => p.Category).Include(p => p.Inspirator).Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == postId);

            return post;
        }
        public async Task<PostEditDeleteModel> FindPostEditDeleteAsync(string postId)
        {
            var post = await this.context.Posts.Include(p => p.Category).Include(p => p.Inspirator).Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == postId);

            return new PostEditDeleteModel()
            {
                Id = post.Id,
                Quote = post.Quote,
                Category = post.Category,
                Author = post.Author,
                Inspirator = post.Inspirator
            };
        }
    }
}
