using AutoMapper;
using BusinessLogicLibrary.Dtos;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLibrary.PostBLL
{
    public class PostBLL : IPostBLL
    {
        private readonly BlogAppContext _context;
        private readonly IMapper _mapper;

        public PostBLL(BlogAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadPostViewModel>> GetPostsDescendingAsync()
        {

            var posts = await _context.Posts.OrderByDescending(x => x.Id).ToListAsync();
            foreach (var post in posts)
            {
                post.Author = _context.BlogAppUsers.Find(post.AuthorId);
            }

            return _mapper.Map<IEnumerable<ReadPostViewModel>>(posts);
        }

        public async Task<bool> AddPostAsync(AddPostViewModel model, string authorId)
        {
            var post = _mapper.Map<Post>(model);

            post.Author = await _context.Users.FindAsync(authorId);

            if (post == null)
                throw new Exception("Model is empty");

            _context.Add(post);
            var success = await _context.SaveChangesAsync() > 0;

            if (success)
                return true;

            throw new Exception("Problem saving changes");
        }
        public IEnumerable<MyPostViewModel> GetUserPosts(string userId)
        {
            var myPosts = _context.Posts.Where(r => userId.Contains(r.AuthorId));
            var model = _mapper.Map<IEnumerable<MyPostViewModel>>(myPosts);
            return model;
        }

        public async Task<EditPostViewModel> GetByIdAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            return _mapper.Map<EditPostViewModel>(post);
        }

        public async Task<bool> EditPostAsync(EditPostViewModel post, string authorId)
        {
            var model = _mapper.Map<Post>(post);
            model.AuthorId = authorId;

            var entity = await _context.Posts.FindAsync(model.Id);

            _context.Entry(entity).CurrentValues.SetValues(model);

            var success = await _context.SaveChangesAsync() > 0;

            if (success)
                return true;

            return false;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var entity = await _context.Posts.FindAsync(id);

            if (entity == null)
                throw new Exception("Could not find post");

            _context.Remove(entity);

            var success = await _context.SaveChangesAsync() > 0;
            if (success)
                return true;

            throw new Exception("Problem saving changes");
        }

        public void DeleteUserPosts(string userId)
        {
            var myPosts = _context.Posts.Where(r => userId.Contains(r.AuthorId));
            foreach (var post in myPosts)
                _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
