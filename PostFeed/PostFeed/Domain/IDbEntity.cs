using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeed.Domain
{
    /// <summary>
    /// Allows an object to be stored in database with Key Id.
    /// Requires int Id
    /// </summary>
    public interface IDbEntity
    {
        int Id { get; set; }
    }
}
