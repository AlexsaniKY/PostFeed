using System.Collections.Generic;

namespace PostFeed.Domain
{
    public class Author: IActivatable, IDbEntity
    {
        public int Id { get; set; }
        public bool Active { get; set; }

        public string Name { get; set; }

        ICollection<Post> Posts { get; set; }

        ICollection<> DeleteCascade()
        {
            foreach(Post p in Posts)
            {
                p.Active = false;
            }
            return Posts;
        }
    }
}