using PostFeed.Views.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostFeed.Domain
{
    /// <summary>
    /// Author represents someone who writes a Post
    /// </summary>
    public class Author: IActivatable, IDbEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Active { get; set; }
        /// <summary>
        /// Name of Author
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// All posts created by Author
        /// </summary>
        public ICollection<Post> Posts { get; set; }


        public Author(){}

        public Author(int id, bool active, string name, ICollection<Post> posts)
        {
            Id = id;
            Active = active;
            Name = name;
            Posts = posts;
        }

        /// <summary>
        /// Create Author from AuthorViewModel
        /// </summary>
        /// <param name="author"></param>
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

        /// <summary>
        /// Allows delete cascades without removing data, may not work with EF yet
        /// </summary>
        public void DeleteCascade()
        {
            foreach(Post p in Posts)
            {
                p.Active = false;
            }
        }
    }
}