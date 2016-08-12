using PostFeed.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostFeed.Domain
{
    public class Post: IActivatable, IDbEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Active { get; set; }

        public string Title { get; set; }
        [Required]
        public string BodyText { get; set; }
        public DateTime TimePosted { get; set; }

        [Required]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author PostCreator { get; set; }

        public void DeleteCascade() { }

        public Post() { }
        public Post(PostViewModel post)
        {
            Id = post.Id;
            Active = post.Active;
            Title = post.Title;
            BodyText = post.BodyText;
            TimePosted = post.TimePosted;
            AuthorId = post.AuthorId;
        }
    }
}