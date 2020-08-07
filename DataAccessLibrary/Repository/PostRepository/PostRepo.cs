using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using System;

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

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
