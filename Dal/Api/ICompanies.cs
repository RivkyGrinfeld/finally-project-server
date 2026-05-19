using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface ICompanies:ICrud<CompaniesTbl>
    {
        public Task<CompaniesTbl> Get(int id);
        public Task<CompaniesTbl> GetByUserId(int userId);
    }
}
