using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using DataAccessLibrary.Models;
using AutoMapper;
using System.Security.Claims;
using DataAccessLibrary.Repository.PostRepository;
using DataAccessLibrary.Repository.UserRepository;
using System.Collections.Generic;
using System.Linq;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepo _postRepository;
        public readonly IMapper _mapper;
        private readonly IUserRepo _userRepository;

        public HomeController(ILogger<HomeController> logger, IPostRepo postRepository, IMapper mapper, IUserRepo userRepository)
        {
            _logger = logger;
            _postRepository = postRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllDescending();
            foreach (var post in posts)
            {
                post.Author = _userRepository.GetById(post.AuthorId);
            }
            var model = _mapper.Map<IEnumerable<ReadPostViewModel>>(posts);
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddPost(AddPostViewModel model)
        {
            var postModel = _mapper.Map<Post>(model);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            postModel.Author = _userRepository.GetById(userId);
            _postRepository.CreatePost(postModel);

            ViewBag.ActionDone = "Post został dodany";

            return base.View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult MyPosts()
        {
            var model = GetMyPosts();

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var post = _postRepository.GetById(Id);
            var model = _mapper.Map<EditPostViewModel>(post);

            return View("EditMyPost", model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(EditPostViewModel editedPost)
        {
            var model = _mapper.Map<Post>(editedPost);

            model.AuthorId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _postRepository.Update(model);


            var myPosts = GetMyPosts();
            return View("MyPosts", myPosts);
        }


        [Authorize]
        public ActionResult Delete(int Id)
        {
           _postRepository.DeleteById(Id);
            
            var model = GetMyPosts();

            return View("MyPosts", model);
        }
        
        private IEnumerable<MyPostViewModel> GetMyPosts()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var myPosts = _postRepository.GetAllByUserId(userId);

            var model = _mapper.Map<IEnumerable<MyPostViewModel>>(myPosts);
            return model;
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
