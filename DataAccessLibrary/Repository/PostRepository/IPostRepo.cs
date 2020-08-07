using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repository.PostRepository
{
    public interface IPostRepo
    {
        public void SaveChanges();
        public void CreatePost(Post post);
    }
}
