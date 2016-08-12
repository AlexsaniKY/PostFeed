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

        public List<Post> GetRecent(int amount, TimeSpan recency)
        {
            DateTime recentDate = DateTime.Now.Subtract(recency);
            return GetAll()
                //.GroupBy(p => p.TimePosted > recentDate)
                .Where(p => p.TimePosted > recentDate)
                //.SelectMany(p => p)
                .OrderByDescending(p => p.TimePosted)
                .Take(amount)
                .ToList();
        }
    }
}