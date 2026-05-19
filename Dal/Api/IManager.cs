using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IManager:ICrud<ManagersTbl>
    {
        public Task<ManagersTbl> Get(int id);
        public Task<ManagersTbl> GetByUserId(int userId);
    }
}
