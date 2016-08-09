using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeed.Domain
{
    interface IActivatable
    {
        bool Active { get; set; }
        ICollection<> DeleteCascade();
    }
}
