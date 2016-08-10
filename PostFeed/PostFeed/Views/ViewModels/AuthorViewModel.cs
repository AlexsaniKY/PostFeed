using PostFeed.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostFeed.Views.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }

        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
        public AuthorViewModel() { }
        public AuthorViewModel(Author author)
        {
            Id = author.Id;
            Active = author.Active;
            Name = author.Name;
            Posts = author.Posts;
        }
    }
}