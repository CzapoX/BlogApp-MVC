using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repository.UserRepository
{
    public class UserRepo : IUserRepo
    {
        private readonly BlogAppContext _context;
        public UserRepo(BlogAppContext context)
        {
            _context = context;
        }
        public BlogAppUser GetById(string id)
        {
            return _context.Set<BlogAppUser>().Find(id);
        }
    }
}
