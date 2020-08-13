using DataAccessLibrary.Models;
using System.Collections.Generic;

namespace DataAccessLibrary.Repository.PostRepository
{
    public interface IPostRepo
    {
        public void CreatePost(Post post);
        public IEnumerable<Post> GetAllDescending();
        public IEnumerable<Post> GetAllByUserId(string id);
        public Post GetById(int id);
        public void DeleteById(int id);
        public void DeleteFromList(ICollection<Post> posts);
        void Update(Post model);
    }
}
