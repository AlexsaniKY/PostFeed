using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostFeed.Domain
{
    public class Post: IActivatable, IDbEntity
    {
        public int Id { get; set; }
        public bool Active { get; set; }

        public string Title { get; set; }
        public string BodyText { get; set; }
        public DateTime TimePosted { get; set; }

        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author PostCreator { get; set; }

        ICollection<> DeleteCascade() { }
    }
}