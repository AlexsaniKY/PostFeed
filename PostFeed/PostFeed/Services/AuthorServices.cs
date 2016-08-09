using PostFeed.Domain;
using PostFeed.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostFeed.Services
{
    public class AuthorServices: GenericServices<Author>
    {
        public AuthorServices()
        {
            _repo = new AuthorRepository(PostFeedDbContext.Create());
        }
    }
}