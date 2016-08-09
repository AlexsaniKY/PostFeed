using PostFeed.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostFeed.Infrastructure
{
    public class PostRepository: GenericRepository<Post>
    {
        public PostRepository(PostFeedDbContext db): base(db) { }
    }
}