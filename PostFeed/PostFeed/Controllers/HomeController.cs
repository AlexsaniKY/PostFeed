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
            List<Post> recentPosts = postServices.GetRecent(10, new TimeSpan(1, 0, 0, 0));
            List<PostViewModel> allPostViewModels = new List<PostViewModel>();
            PostViewModel postViewModel;
            foreach (Post p in recentPosts)
            {
                postViewModel = new PostViewModel(p);
                postViewModel.PostCreator = new AuthorViewModel(authorServices.Get(p.AuthorId));
                allPostViewModels.Add(postViewModel);
            }
            return View(allPostViewModels);
            //return View(GetRecentPosts());
        }

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