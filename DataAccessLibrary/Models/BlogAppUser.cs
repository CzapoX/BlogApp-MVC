using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLibrary.Models
{
    // Add profile data for application users by adding properties to the BlogAppUser class
    public class BlogAppUser : IdentityUser
    {
        public ICollection<Post> Posts { get; set; }
    }
}
