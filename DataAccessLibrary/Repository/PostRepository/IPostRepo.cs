using DataAccessLibrary.Models;
using System.Collections.Generic;

namespace DataAccessLibrary.Repository.PostRepository
{
    public interface IPostRepo
    {
        public void CreatePost(Post post);
        public IEnumerable<Post> GetAll();
        public void SaveChanges();
    }
}
