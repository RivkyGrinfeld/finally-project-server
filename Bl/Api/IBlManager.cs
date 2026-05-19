using Bl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    public interface IBlManager:IBlCrud<BlManagers>
    {
        public Task<BlManagers> Get(int id);
        public Task<BlManagers> GetByUserId(int userId);
    }
}
