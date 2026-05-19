using Bl.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    public interface IBlPosts:IBlCrud<BlPosts>
    {
        public Task<BlPosts> Get(int id);

        public void ConfirmPost(int  id);

    }
}
