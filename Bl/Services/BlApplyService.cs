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
    public class BlApplyService : IBlApply
    {
        IDal dal;
        public BlApplyService(IDal dal)
        {
            this.dal = dal;

        }
        public Task<bool> Create(BlApply t)
        {
            return dal.Apply.Create(Converts.ConvertFromBlApplyToApply(t)); 
        }

        public Task<bool> Delete(BlApply t)
        {
            throw new NotImplementedException();
        }

        public IBlApply Get(BlApply entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BlApply>> GetAll()
        {
            List<BlApply> blApplies = new List<BlApply>();
            dal.Apply.GetAll().Result.ForEach(x => blApplies.Add(Converts.ConvertFromApplyToBlApply(x)));
            return blApplies;
        }

        public Task<BlApply> GetById(int t)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(BlApply t)
        {
            return await dal.Apply.Update(Converts.ConvertFromBlApplyToApply(t));
        }
    }
}
