using PostFeed.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostFeed.Infrastructure
{
    public class AuthorRepository: GenericRepository<Author>
    {
        public AuthorRepository(PostFeedDbContext db): base(db) { }
    }
}