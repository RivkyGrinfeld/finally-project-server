using Bl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    public interface IBlStatus:IBlCrud<BlStatus>
    {
        public Task<BlStatus> Get(int id);
    }
}
