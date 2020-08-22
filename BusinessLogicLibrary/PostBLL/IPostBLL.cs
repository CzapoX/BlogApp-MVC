using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLibrary.Dtos;

namespace BusinessLogicLibrary.PostBLL
{
    public interface IPostBLL
    {
        Task<bool> AddPostAsync(AddPostViewModel model, string authorId);
        Task<IEnumerable<ReadPostViewModel>> GetPostsDescendingAsync();
        IEnumerable<MyPostViewModel> GetUserPosts(string userId);
        Task<EditPostViewModel> GetByIdAsync(int id);
        Task<bool> EditPostAsync(EditPostViewModel post, string authorId);
        Task<bool> DeletePostAsync(int id);
        void DeleteUserPosts(string userId);
    }
}