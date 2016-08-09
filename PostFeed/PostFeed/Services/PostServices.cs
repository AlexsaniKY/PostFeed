using PostFeed.Domain;
using PostFeed.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostFeed.Services
{
    public class PostServices: GenericServices<Post>
    {
        public PostServices()
        {
            _repo = new PostRepository(PostFeedDbContext.Create());
        }
    }
}