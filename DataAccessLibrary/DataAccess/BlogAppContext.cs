using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.DataAccess
{
    public class BlogAppContext : IdentityDbContext<BlogAppUser>
    {
        public BlogAppContext(DbContextOptions<BlogAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

        public DbSet<BlogAppUser> BlogAppUsers { get; set; }
        public DbSet<Post> Posts { get; set; }

    }
}
