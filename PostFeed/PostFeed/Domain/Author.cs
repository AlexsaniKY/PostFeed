using PostFeed.Views.ViewModels;
using System.Collections.Generic;

namespace PostFeed.Domain
{
    public class Author: IActivatable, IDbEntity
    {
        public int Id { get; set; }
        public bool Active { get; set; }

        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }

        public void DeleteCascade()
        {
            foreach(Post p in Posts)
            {
                p.Active = false;
            }
        }

        public Author(){}
        public Author(int id, bool active, string name, ICollection<Post> posts)
        {
            Id = id;
            Active = active;
            Name = name;
            Posts = posts;
        }
        public Author(AuthorViewModel author)
        {
            Id = author.Id;
            Active = author.Active;
            Name = author.Name;
            Posts = author.Posts;
        }
    }
}