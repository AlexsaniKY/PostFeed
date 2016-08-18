using PostFeed.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostFeed.Domain
{
    /// <summary>
    /// Post is a message created by an Author on any desired subject
    /// </summary>
    public class Post: IActivatable, IDbEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Active { get; set; }
        /// <summary>
        /// Title of the Post, to be displayed as a large header
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Body of the Post, where most of the information should go
        /// </summary>
        [Required]
        public string BodyText { get; set; }
        /// <summary>
        /// Time of the original Post.  Determined by server.
        /// </summary>
        public DateTime TimePosted { get; set; }
        /// <summary>
        /// Id of the creator of this Post
        /// </summary>
        [Required]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author PostCreator { get; set; }
        /// <summary>
        /// This object owns no objects, no DeleteCascade necessary
        /// </summary>
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