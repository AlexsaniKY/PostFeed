using PostFeed.Views.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostFeed.Domain
{
    public class Author: IActivatable, IDbEntity
    {
        [Required]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Active { get; set; }
        [Required]
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
            Posts = new List<Post>();
            if(author.Posts != null)
                foreach (PostViewModel p in author.Posts)
                {
                    Posts.Add(new Post(p));
                }
        }
    }
}