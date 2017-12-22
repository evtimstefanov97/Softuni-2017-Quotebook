using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuoteBook.Data;
using QuoteBook.Data.Models;
using QuoteBook.Extensions;
using Microsoft.AspNetCore.Identity;
using QuoteBook.Areas.Quote.Models;
using QuoteBook.Services.CategoryService;
using QuoteBook.Services.InspiratorService;
using Microsoft.AspNetCore.Authorization;
using QuoteBook.Services.PostsService;
using QuoteBook.Services.PostsService.Models;

namespace QuoteBook.Areas.Quote.Controllers
{
    [Area("UserQuote")]
    [Authorize(Roles = "Author,Administrator")]
    public class PostsController : Controller
    {
        private readonly QuoteBookDbContext context;
        private readonly UserManager<User> userManager;
        private readonly ICategoryService categoryservice;
        private readonly IInspiratorService inspiratorService;
        private readonly IPostsService postsService;
        public PostsController(QuoteBookDbContext context, UserManager<User> userManager, ICategoryService categoryService, IInspiratorService inspiratorService, IPostsService postsService)
        {
            this.context = context;
            this.userManager = userManager;
            this.categoryservice = categoryService;
            this.inspiratorService = inspiratorService;
            this.postsService = postsService;
        }

        // GET: Posts/Posts

        public async Task<IActionResult> UserQuotes(string userIdForAdminActions)
        {
            if (userIdForAdminActions?.Length > 0)
            {
                var userQuotesAdminActions = await this.postsService.AllByUser(userIdForAdminActions);
                TempData["userExternalId"] = userIdForAdminActions;

                return View(userQuotesAdminActions);
            }
            
                var userId = this.User.GetUserId();
                var userQuotes = await this.postsService.AllByUser(userId);

                return View(userQuotes);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await this.postsService.FindPostDetailsAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

 

        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(context.Categories, "Id", "Title");
            ViewData["Inspirators"] = new SelectList(context.Inspirators, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(AddPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = (await userManager.GetUserAsync(User));
                var category = await this.categoryservice.FindCategoryAsync(model.CategoryId);
                var inspirator = await this.inspiratorService.FindInspiratorAsync(model.InspiratorId);
                await this.postsService.CreatePostAsync(author, category, inspirator, model.Quote);
                return RedirectToAction(nameof(UserQuotes));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await this.postsService.FindPostEditDeleteAsync(id);
            TempData["userExternalId"] = post.Author.Id;

            if (post == null)
            {
                return NotFound();
            }
            if (post.Inspirator != null && post.Category!=null)
            {
                ViewData["Categories"] = new SelectList(context.Categories, "Id", "Title", post.Category.Id);
                ViewData["Inspirators"] = new SelectList(context.Inspirators, "Id", "Name", post.Inspirator.Id);

            }
            else
            {

                ViewData["Categories"] = new SelectList(context.Categories, "Id", "Title");
                ViewData["Inspirators"] = new SelectList(context.Inspirators, "Id", "Name");
            }
          
            return View(post);
        }
        public async Task<IActionResult> EditConfirm(PostEditDeleteModel model)
        {
            var category = await this.categoryservice.FindCategoryAsync(model.Category.Id);
            var inspirator = await this.inspiratorService.FindInspiratorAsync(model.Inspirator.Id);
            var quote = model.Quote;
            var postId = model.Id;
            var author = model.Author;

            await this.postsService.EditPostAsync(category,inspirator,quote,postId, author);
            var externalUserId = TempData["userExternalId"].ToString();

            return RedirectToAction("UserQuotes", "Posts", new { userIdForAdminActions = externalUserId });
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await this.postsService.FindPostEditDeleteAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id,string userIdForAdminActions)
        {
            await this.postsService.DeletePostAsync(id);

            return RedirectToAction("UserQuotes","Posts",new { userIdForAdminActions = userIdForAdminActions });
        }

        private bool PostExists(string id)
        {
            return context.Posts.Any(e => e.Id == id);
        }
    }
}
