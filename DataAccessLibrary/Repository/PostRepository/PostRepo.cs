using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
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
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public void DeleteFromList(ICollection<Post> posts)
        {
            foreach (var post in posts)
                _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetAllDescending()
        {
            return _context.Set<Post>().OrderByDescending(p => p.Id).ToList();
        }

        public IEnumerable<Post> GetAllByUserId(string id)
        {
            var model = _context.Posts.Where(r => id.Contains(r.AuthorId));
            return model.ToList();
        }

        public Post GetById(int id)
        {
            return _context.Set<Post>().Find(id);
        }

        public void Update(Post model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.Attach(model);

            _context.SaveChanges();
        }
    }
}
