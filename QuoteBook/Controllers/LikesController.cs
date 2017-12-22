using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using QuoteBook.Services.PostsService;
using QuoteBook.Services.LikesService;
using QuoteBook.Data.Models;
using QuoteBook.Extensions;
using QuoteBook.Services.LikesService.Models;

namespace QuoteBook.Controllers
{
    public class LikesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IPostsService postsService;
        private readonly ILikesService likesService;
        public LikesController(IPostsService postsService, ILikesService likesService, UserManager<User> userManager)
        {
            this.postsService = postsService;
            this.likesService = likesService;
            this.userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Like(string PostId, [Bind("PostId")] LikeViewModel model)
        {
            var user = await this.userManager.GetUserAsync(User);
            
        
            await likesService.Like(model.PostId, user);
            return RedirectToAction("AllQuotes","Posts",new { orderColumn = "Likes", type = "Asc" });
        }
    }
}