using PostFeed.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PostFeed.Infrastructure
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(): base("AppDbContext")
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }

    }
}