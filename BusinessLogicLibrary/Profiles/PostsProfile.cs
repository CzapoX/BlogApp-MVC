using AutoMapper;
using BusinessLogicLibrary.Dtos;
using DataAccessLibrary.Models;

namespace BusinessLogicLibrary.Profiles
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<AddPostViewModel, Post>();
            CreateMap<Post, ReadPostViewModel>();
            CreateMap<Post, MyPostViewModel>();
            CreateMap<Post, EditPostViewModel>();
            CreateMap<EditPostViewModel, Post>();
        }

    }
}
