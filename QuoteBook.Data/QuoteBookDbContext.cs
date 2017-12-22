using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuoteBook.Data.Models;

namespace QuoteBook.Data
{
    public class QuoteBookDbContext : IdentityDbContext<User>
    {
        public QuoteBookDbContext(DbContextOptions<QuoteBookDbContext> options)
            : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Inspirator> Inspirators { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);


            builder.Entity<Inspirator>()
                .HasMany(i => i.Posts)
                .WithOne(p => p.Inspirator)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Post>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Post>()
                .HasMany(p => p.Likes)
                .WithOne(c => c.Post)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasMany(u => u.Likes)
                .WithOne(l => l.User).
                HasForeignKey(l => l.UserId);

            base.OnModelCreating(builder);
        }
    }
}
