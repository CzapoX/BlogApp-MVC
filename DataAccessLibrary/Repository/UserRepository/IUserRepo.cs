using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Repository.UserRepository
{
    public interface IUserRepo
    {
        public BlogAppUser Get(string id);
    }
}
