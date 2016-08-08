using PostFeed.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PostFeed.Infrastructure
{
    public class PostFeedDbContext: DbContext
    {
        public PostFeedDbContext(): base("PostFeedDbContext")
        {
            Database.SetInitializer<PostFeedDbContext>(null);
        }

        public static PostFeedDbContext Create()
        {
            return new PostFeedDbContext();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }

    }
}