using Bl.Api;
using Bl.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class BlBranchesService : IBlBranches
    {
        IDal dal;
        public BlBranchesService(IDal dal)
        {
            this.dal = dal;

        }
        public async Task<bool> Create(BlBranches t)
        {
            return await dal.Branches.Create(Converts.ConvertFromBlBranchToBranch(t));

        }
        public async Task<bool> Delete(BlBranches t)
        {
           return await dal.Branches.Delete(Converts.ConvertFromBlBranchToBranch(t));
             
        }
        public async Task<BlBranches> Get(int id)
        {
            return Converts.ConvertFromBranchToBlBranch(dal.Branches.Get(id).Result);
        }
        public async Task<List<BlBranches>> GetAll()
        {
            List<BlBranches> blList = new List<BlBranches>();
            dal.Branches.GetAll().Result.ForEach(c => blList.Add(Converts.ConvertFromBranchToBlBranch(c)));
            return blList;
        }
        public async Task<bool> Update(BlBranches t)
        {
           return await dal.Branches.Update(Converts.ConvertFromBlBranchToBranch(t));          
        }
        public async Task<BlBranches> GetById(int t)
        {
            return Converts.ConvertFromBranchToBlBranch(dal.Branches.GetById(t).Result);      
        }

    }
}
