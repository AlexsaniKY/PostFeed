using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeed.Domain
{
    /// <summary>
    /// Allows an object to be "deleted" from a database without being removed.
    /// Requires bool Active and DeleteCascade()
    /// </summary>
    public interface IActivatable
    {
        bool Active { get; set; }
        void DeleteCascade();
    }
}
