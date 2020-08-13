using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLibrary.Repository.PostRepository
{
    public class PostRepo : IPostRepo
    {
        private readonly BlogAppContext _context;
        public PostRepo(BlogAppContext context)
        {
            _context = context;
        }


        public void CreatePost(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            _context.Add(post);
        }

        public void DeleteById(int Id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == Id);
            _context.Posts.Remove(post);
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Set<Post>().ToList();
        }

        public IEnumerable<Post> GetAllByUserId(string Id)
        {
            var model = _context.Posts.Where(r => Id.Contains(r.AuthorId));
            return model.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
