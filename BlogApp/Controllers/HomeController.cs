using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogApp.Models;
using BusinessLogicLibrary.PostBLL;
using BusinessLogicLibrary.Dtos;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostBLL _postBll;

        public HomeController(ILogger<HomeController> logger,IPostBLL postBll)
        {
            _logger = logger;
            _postBll = postBll;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postBll.GetPostsDescendingAsync();
            return View(posts);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var success = await _postBll.AddPostAsync(model, userId);

            if(success)
                ViewBag.ActionDone = "Post został dodany";

            return base.View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult MyPosts()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = _postBll.GetUserPosts(userId);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _postBll.GetByIdAsync(id);

            return View("EditMyPost", model);
        }

        [Authorize]
        public async Task<ActionResult> Edit(EditPostViewModel editedPost)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var success = await _postBll.EditPostAsync(editedPost, userId);

            if(success)
                ViewBag.UpdateSuccess = "Post został edytowany";

            if (!success)
                ViewBag.UpdateFail = "Post nie został edytowany";

            var myPosts = _postBll.GetUserPosts(userId);

            return View("MyPosts", myPosts);
        }


        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _postBll.DeletePostAsync(id);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var model = _postBll.GetUserPosts(userId);

            return View("MyPosts", model);
        }
        



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
