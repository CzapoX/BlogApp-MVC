using DataAccessLibrary.Models;
using System.Collections.Generic;

namespace DataAccessLibrary.Repository.PostRepository
{
    public interface IPostRepo
    {
        public void CreatePost(Post post);
        public IEnumerable<Post> GetAll();
        public IEnumerable<Post> GetAllByUserId(string Id);
        public void DeleteById(int Id);
        public void DeleteFromList(ICollection<Post> posts);
    }
}
