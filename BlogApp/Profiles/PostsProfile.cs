using AutoMapper;
using BlogApp.Models;
using DataAccessLibrary.Models;

namespace BlogApp.Profiles
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<AddPostViewModel, Post>();
            CreateMap<Post, ReadPostViewModel>();
            CreateMap<Post, MyPostViewModel>();
        }
        
    }
}
