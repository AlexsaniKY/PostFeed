using PostFeed.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PostFeed.Views.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Title { get; set; }
        [Required]
        public string BodyText { get; set; }
        public DateTime TimePosted { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public AuthorViewModel PostCreator { get; set; }

        public PostViewModel() { }
        public PostViewModel(Post post)
        {
            Id = post.Id;
            Active = post.Active;
            Title = post.Title;
            BodyText = post.BodyText;
            TimePosted = post.TimePosted;
            AuthorId = post.AuthorId;
            //PostCreator = new AuthorViewModel(post.PostCreator);
        }
    }
}