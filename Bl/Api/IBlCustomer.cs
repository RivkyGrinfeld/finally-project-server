using Bl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    public interface IBlCustomer: IBlCrud<BlCustomer>
    {
        public Task<BlCustomer> Get(string id);
        public Task<BlCustomer> GetByUserId(int userId);

    }
}
