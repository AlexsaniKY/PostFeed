using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeed.Domain
{
    public interface IActivatable
    {
        bool Active { get; set; }
        void DeleteCascade();
    }
}
