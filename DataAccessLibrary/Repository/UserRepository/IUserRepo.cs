using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repository.UserRepository
{
    public interface IUserRepo
    {
        public BlogAppUser GetById(string id);
    }
}
