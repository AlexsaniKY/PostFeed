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

        /// <summary>
        /// Converts an IEnumerable of Posts to PostViewModels
        /// </summary>
        /// <param name="inputList">a sequence of Posts to convert</param>
        /// <returns>IEnumerable of PostViewModels</returns>
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

        /// <summary>
        /// Converts an IEnumerable of PostViewModels to Posts
        /// </summary>
        /// <param name="inputList">a sequence of PostViewModels to convert</param>
        /// <returns>IEnumerable of Posts</returns>
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

        /// <summary>
        /// Adds an Author Entity to each Post in an IEnumerable of Posts
        /// through AuthorServices
        /// </summary>
        /// <param name="postCollection">IEnumerable of Posts to add Authors to</param>
        public void PopulateAuthors(IEnumerable<Post> postCollection)
        {
            foreach (Post post in postCollection)
            {
                if(post.PostCreator == null)
                    post.PostCreator =
                        authorServices.Get(post.AuthorId);
            }
        }

        /// <summary>
        /// Adds an Author Entity to each PostViewModel in an IEnumerable of
        /// PostViewModels through AuthorServices
        /// </summary>
        /// <param name="postCollection">IEnumerable of PostViewModels to add Authors to</param>
        public void PopulateAuthors(IEnumerable<PostViewModel> postCollection)
        {
            foreach (PostViewModel postvm in postCollection)
            {
                if(postvm.PostCreator == null)
                    postvm.PostCreator = new AuthorViewModel(
                        authorServices.Get(postvm.AuthorId)
                            );
            }
        }

        /// <summary>
        /// Get Recent Posts in Descending order
        /// </summary>
        /// <param name="amount">how many posts to return</param>
        /// <returns>IQueryable of an amount of Posts</returns>
        public IQueryable<Post> GetRecent(int amount)
        {
            return GetAll()
                .OrderByDescending(p => p.TimePosted)
                .Take(amount);
        }

        /// <summary>
        /// Get Recent Posts in Descending order
        /// </summary>
        /// <param name="amount">how many posts to return</param>
        /// <param name="recency">a time span for how far back to retrieve posts from now</param>
        /// <returns>IQueryable of an amount of Posts</returns>
        public IQueryable<Post> GetRecent(int amount, TimeSpan recency)
        {
            DateTime recentDate = DateTime.Now.Subtract(recency);
            return GetPostsAfter(amount, recentDate);
        }

        /// <summary>
        /// Get Posts created after a Post with given Id
        /// </summary>
        /// <param name="id">Id of the Post to use as a cutoff</param>
        /// <returns>IQueryable of Posts after the given Post</returns>
        public IQueryable<Post> GetPostsAfter(int id)
        {
            Post post = Get(id);
            return GetAll()
                .Where(p => p.TimePosted > post.TimePosted)
                .OrderByDescending(p => p.TimePosted);
        }

        /// <summary>
        /// Get an amount of Posts after a given Id
        /// </summary>
        /// <param name="amount">amount of Posts to retrieve</param>
        /// <param name="id">Id of the Post to use as a cutoff</param>
        /// <returns>IQueryable of an amount of Posts after the given Post</returns>
        public IQueryable<Post> GetPostsAfter(int amount, int id)
        {
            return GetPostsAfter(id)
                .Take(amount);
        }

        /// <summary>
        /// Get all Posts after a specified DateTime
        /// </summary>
        /// <param name="sinceDate">cutoff DateTime</param>
        /// <returns>IQueryable of Posts after the given DateTime</returns>
        public IQueryable<Post> GetPostsAfter(DateTime sinceDate)
        {
            return GetAll()
                .Where(p => p.TimePosted > sinceDate)
                .OrderByDescending(p => p.TimePosted);
        }

        /// <summary>
        /// Get an amount of Posts after a specified DateTime
        /// </summary>
        /// <param name="amount">amount of Posts to retrieve</param>
        /// <param name="sinceDate">cutoff DateTime</param>
        /// <returns>IQueryable of an amount of Posts after the given DateTime</returns>
        public IQueryable<Post> GetPostsAfter(int amount, DateTime sinceDate)
        {
            return GetPostsAfter(sinceDate)
                .Take(amount);
        }

        /// <summary>
        /// Get Posts before a Post with the given Id
        /// </summary>
        /// <param name="id">Id of the Post to use as a cutoff</param>
        /// <returns>IQueryable of Posts before the given Post</returns>
        public IQueryable<Post> GetPostsBefore(int id)
        {
            Post post = Get(id);
            return GetAll()
                .Where(p => p.TimePosted < post.TimePosted)
                .OrderByDescending(p => p.TimePosted);
        }

        /// <summary>
        /// Get an amount of Posts before a Post with given Id
        /// </summary>
        /// <param name="amount">amount of Posts to retrieve</param>
        /// <param name="id">Id of the Post to use as a cutoff</param>
        /// <returns>IQueryable of an amount of Posts before the given Post</returns>
        public IQueryable<Post> GetPostsBefore(int amount, int id)
        {
            return GetPostsBefore(id)
                .Take(amount);
        }

        /// <summary>
        /// Get Posts before a given DateTime
        /// </summary>
        /// <param name="beforeDate">DateTime to use as a cutoff</param>
        /// <returns>IQueryable of Posts before the given DateTime</returns>
        public IQueryable<Post> GetPostsBefore(DateTime beforeDate)
        {
            return GetAll()
                .Where(p => p.TimePosted < beforeDate)
                .OrderByDescending(p => p.TimePosted);
        }

        /// <summary>
        /// Get an amount of Posts before a given DateTime
        /// </summary>
        /// <param name="amount">amount of Posts to retrieve</param>
        /// <param name="beforeDate">DateTime to use as a cutoff</param>
        /// <returns>IQueryable of an amount of Posts before the given DateTime</returns>
        public IQueryable<Post> GetPostsBefore(int amount, DateTime beforeDate)
        {
            return GetPostsBefore(beforeDate)
                .Take(amount);

        }
    }
}