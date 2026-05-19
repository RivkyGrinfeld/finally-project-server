using Bl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    public interface IBlCompanies:IBlCrud<BlCompanies>
    {
        public Task<BlCompanies> Get(int id);

        public Task<List<BlPosts>> GetMyPosts(int id );

        public Task<BlCompanies> GetByUserId(int userId);
    }
}
