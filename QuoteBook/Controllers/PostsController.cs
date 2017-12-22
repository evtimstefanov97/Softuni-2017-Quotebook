using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuoteBook.Services.CategoryService;
using Microsoft.AspNetCore.Identity;
using QuoteBook.Data;
using QuoteBook.Services.InspiratorService;
using QuoteBook.Services.PostsService;
using QuoteBook.Data.Models;

namespace QuoteBook.Controllers
{
    public class PostsController : Controller
    {
        private readonly QuoteBookDbContext context;
        private readonly UserManager<User> userManager;
        private readonly ICategoryService categoryservice;
        private readonly IPostsService postsService;
        public PostsController(QuoteBookDbContext context, UserManager<User> userManager, ICategoryService categoryService,IPostsService postsService)
        {
            this.context = context;
            this.userManager = userManager;
            this.categoryservice = categoryService;
            this.postsService = postsService;
        }
 
        public async Task<IActionResult> AllQuotes(string orderColumn,string type)
        {
            if(orderColumn?.Length>0 && type?.Length > 0 && type!="All")
            {
                var allQuotesOrdered = await this.postsService.AllOrdered(orderColumn, type);
                return PartialView("_AllQuotesPartial",allQuotesOrdered);
            }
            var allQuotes = await this.postsService.All();
            var categoriesForSort = await this.categoryservice.All();

            ViewData["CategoriesForSort"] = categoriesForSort;
            if (type == "All")
            {
                
                return PartialView("_AllQuotesPartial",allQuotes);
            }
          
            return View(allQuotes);
        }
    }
}