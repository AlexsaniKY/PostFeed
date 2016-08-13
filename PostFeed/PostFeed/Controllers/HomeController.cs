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

        public ActionResult Index()
        {
            IEnumerable<Post> recentPosts = postServices.GetRecent(10, new TimeSpan(1, 0, 0, 0));
            IEnumerable<PostViewModel> allPostViewModels =
                postServices.PostCollectionToPostVMEnum(
                recentPosts).ToList();
            postServices.PopulateAuthors(allPostViewModels);
            return View(allPostViewModels);

        }

        [HttpGet]
        public PartialViewResult PartialPost(int id)
        {
            PostViewModel postViewModel = new PostViewModel(postServices.Get(id));
            postViewModel.PostCreator = new AuthorViewModel(authorServices.Get(postViewModel.AuthorId));
            return PartialView("_PostPartial", postViewModel);
        } 

        //[HttpGet]
        //public PartialViewResult PartialPostRange()


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


        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}