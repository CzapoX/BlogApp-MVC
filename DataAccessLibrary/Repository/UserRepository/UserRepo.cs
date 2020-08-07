using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Repository.UserRepository
{
    public class UserRepo : IUserRepo
    {
        private readonly BlogAppContext _context;
        public UserRepo(BlogAppContext context)
        {
            _context = context;
        }
        public BlogAppUser Get(string id)
        {
            return _context.Set<BlogAppUser>().Find(id);
        }
    }
}
