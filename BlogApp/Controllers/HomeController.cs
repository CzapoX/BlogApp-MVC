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
            var posts = _postRepository.GetAll();
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

        [HttpPost]
        public IActionResult AddPost(AddPostViewModel model)
        {
            var postModel = _mapper.Map<Post>(model);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            postModel.Author = _userRepository.GetById(userId);
            _postRepository.CreatePost(postModel);
            _postRepository.SaveChanges();

            return View("Index", model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
