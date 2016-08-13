using PostFeed.Domain;
using PostFeed.Infrastructure;
using PostFeed.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostFeed.Services
{
    public class PostServices: GenericServices<Post>
    {
        internal AuthorServices authorServices = new AuthorServices();
        public PostServices()
        {
            _repo = new PostRepository(PostFeedDbContext.Create());
        }

        public IEnumerable<PostViewModel> PostCollectionToPostVMEnum(IEnumerable<Post> inputList)
        {
            return (from post in inputList
                    select new PostViewModel
                    {
                        Id = post.Id,
                        Active = post.Active,
                        Title = post.Title,
                        BodyText = post.BodyText,
                        TimePosted = post.TimePosted,
                        AuthorId = post.AuthorId
                    });
        }

        public IEnumerable<Post> PostVMCollectionToPostEnum(IEnumerable<PostViewModel> inputList)
        {
            return (from postvm in inputList
                    select new Post
                    {
                        Id = postvm.Id,
                        Active = postvm.Active,
                        Title = postvm.Title,
                        BodyText = postvm.BodyText,
                        TimePosted = postvm.TimePosted,
                        AuthorId = postvm.AuthorId
                    });
        }

        public void PopulateAuthors(IEnumerable<Post> postCollection)
        {
            foreach (Post post in postCollection)
            {
                post.PostCreator =
                    authorServices.Get(post.AuthorId);
            }
        }

        public void PopulateAuthors(IEnumerable<PostViewModel> postCollection)
        {
            foreach (PostViewModel postvm in postCollection)
            {
                postvm.PostCreator = new AuthorViewModel(
                    authorServices.Get(postvm.AuthorId)
                        );
            }
        }

        public IQueryable<Post> GetRecent(int amount)
        {
            return GetAll()
                .OrderByDescending(p => p.TimePosted)
                .Take(amount);
        }

        public IQueryable<Post> GetRecent(int amount, TimeSpan recency)
        {
            DateTime recentDate = DateTime.Now.Subtract(recency);
            return GetPostsAfter(amount, recentDate);
        }

        public IQueryable<Post> GetPostsAfter(int amount, DateTime sinceDate)
        {
            return GetAll()
                //.GroupBy(p => p.TimePosted > recentDate)
                .Where(p => p.TimePosted > sinceDate)
                //.SelectMany(p => p)
                .OrderByDescending(p => p.TimePosted)
                .Take(amount);
        }

        public IQueryable<Post> GetPostsBefore(int amount, DateTime beforeDate)
        {
            return GetAll()
                .Where(p => p.TimePosted < beforeDate)
                .OrderByDescending(p => p.TimePosted)
                .Take(amount);

        }
    }
}