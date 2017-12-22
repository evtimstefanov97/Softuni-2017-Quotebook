using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace QuoteBook.Data.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public string FacebookImageUrl { get; set; }

        public byte[] AvatarImage { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
