using PostFeed.Domain;
using PostFeed.Services;
using PostFeed.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PostFeed.Controllers
{
    public class HomeController : Controller
    {
        internal AuthorServices authorServices = new AuthorServices();
        internal PostServices postServices = new PostServices();

        /// <summary>
        /// Returns the main page, and the only complete view.
        /// All content is generated at run time through Api calls
        /// and Partials
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get a Post presented in a Partial View displaying all
        /// necessary 
        /// </summary>
        /// <param name="id">Key Id of Post</param>
        /// <returns>Partial View detailing the Post</returns>
        [HttpGet]
        public PartialViewResult PartialPost(int id)
        {
            PostViewModel postViewModel = new PostViewModel(postServices.Get(id));
            postViewModel.PostCreator = new AuthorViewModel(authorServices.Get(postViewModel.AuthorId));
            return PartialView("_PostPartial", postViewModel);
        }

        /// <summary>
        /// Get a Range of Recent Posts in a sequence of
        /// details views
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>Partial View of multiple PostPartials</returns>
        [HttpGet]
        public PartialViewResult PartialPostRange(int amount)
        {
            //Get all recent posts 
            IEnumerable<Post> recentPosts = postServices.GetRecent(amount);
            //convert recentPosts' Posts to a list of PostViewModels
            IEnumerable<PostViewModel> allPostViewModels =
                postServices.PostCollectionToPostVMEnum(
                    recentPosts).ToList();
            postServices.PopulateAuthors(allPostViewModels);
            return PartialView("_PostPartialRange", allPostViewModels);
        }

        /// <summary>
        /// Get a Range of Recent posts before a Post with given Id
        /// </summary>
        /// <param name="amount">amount of Posts to retrieve</param>
        /// <param name="id">Id of excluded Post to set start point</param>
        /// <returns>Partial View of multiple PostPartials</returns>
        [HttpGet]
        public PartialViewResult PartialPostRangeBeforeId(int? amount, int id)
        {
            IEnumerable<Post> recentPosts;
            if (amount != null)
            {
                recentPosts = postServices.GetPostsBefore(amount ?? default(int), id);
            }
            else
            {
                recentPosts = postServices.GetPostsBefore(id);
            }
            IEnumerable<PostViewModel> allPostViewModels =
                postServices.PostCollectionToPostVMEnum(
                    recentPosts).ToList();
            postServices.PopulateAuthors(allPostViewModels);
            return PartialView("_PostPartialRange", allPostViewModels);
        }

        /// <summary>
        /// Get a Range of Recent posts after a Post with given Id
        /// </summary>
        /// <param name="amount">amount of Posts to retrieve</param>
        /// <param name="id">Id of excluded Post to set start point</param>
        /// <returns>Partial View of multiple PostPartials</returns>
        [HttpGet]
        public PartialViewResult PartialPostRangeAfterID(int? amount, int id)
        {
            IEnumerable<Post> recentPosts;
            if (amount != null)
                recentPosts = postServices.GetPostsAfter(amount ?? default(int), id);
            else recentPosts = postServices.GetPostsAfter(id);
            IEnumerable<PostViewModel> allPostViewModels =
                postServices.PostCollectionToPostVMEnum(
                    recentPosts).ToList();
            postServices.PopulateAuthors(allPostViewModels);
            return PartialView("_PostPartialRange", allPostViewModels);
        }

        /// <summary>
        /// Get Recent Posts
        /// </summary>
        /// <returns>Partial View of all recent Posts</returns>
        private ICollection<PostViewModel> GetRecentPosts()
        {
            var allposts = postServices.GetAll();
            List<PostViewModel> postViewModels = new List<PostViewModel>();
            PostViewModel postView;
            foreach (Post p in allposts)
            {
                postView = new PostViewModel(p);
                postView.PostCreator = new AuthorViewModel(authorServices.Get(p.AuthorId));
                postViewModels.Add(postView);
            }
            postViewModels.Sort((x, y) => DateTime.Compare(y.TimePosted, x.TimePosted));
            return postViewModels;
        }

    }
}