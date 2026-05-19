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
    public class BlManagerService : IBlManager
    {
        IDal dal;
        public BlManagerService(IDal dal)
        {
            this.dal = dal;
        }        

        public async Task<bool> Create(BlManagers t)
        {
            //var b = m.Map<CustomersTbl>(t);

            return await dal.Managers.Create(Converts.ConvertFromBlManagerTomanager(t));

        }
        public async Task<bool> Delete(BlManagers t)
        {
           return await dal.Managers.Delete(Converts.ConvertFromBlManagerTomanager(t));
        }
        public async Task<BlManagers> Get(int id)
        {
            return Converts.ConvertFromManagerToBlManager(dal.Managers.Get(id).Result);
        }
        public async Task<List<BlManagers>> GetAll()
        {
            List<BlManagers> blList = new List<BlManagers>();
            dal.Managers.GetAll().Result.ForEach(c => blList.Add(Converts.ConvertFromManagerToBlManager(c)));
            return blList;
        }
        public async Task<BlManagers> GetById(int t)
        {
            throw new NotImplementedException();
        }

        public async Task<BlManagers> GetByUserId(int userId)
        {
           return Converts.ConvertFromManagerToBlManager(dal.Managers.GetByUserId(userId).Result);
        }

        public async Task<bool> Update(BlManagers t)
        {
           return await dal.Managers.Update(Converts.ConvertFromBlManagerTomanager(t));
        }
    }
}
