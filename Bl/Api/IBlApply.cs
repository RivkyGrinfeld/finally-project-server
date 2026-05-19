using Bl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bl.Api
{
    public interface IBlApply:IBlCrud<BlApply>
    {
        public IBlApply Get(BlApply entity);
    }
}
