using Bl.Api;
using Bl.Models;
using Dal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class BlStatusService:IBlStatus
    {
        IDal dal;
        public BlStatusService(IDal dal)
        {
            this.dal = dal;

        }
        public async Task<bool> Create(BlStatus t)
        {
            return await dal.Status.Create(Converts.ConvertFromBlStatusToStatus(t));

        }
        public async Task<bool> Delete(BlStatus t)
        {
            return await dal.Status.Delete(Converts.ConvertFromBlStatusToStatus(t));
        }
        public async Task<BlStatus> Get(int id)
        {
            return Converts.ConvertFromStatusToBlStatus(dal.Status.Get(id).Result);
        }
        public async Task<List<BlStatus>> GetAll()
        {
            List<BlStatus> blList = new List<BlStatus>();
            dal.Status.GetAll().Result.ForEach(c => blList.Add(Converts.ConvertFromStatusToBlStatus(c)));
            return blList;
        }
        public async Task<bool> Update(BlStatus t)
        {
            return await dal.Status.Update(Converts.ConvertFromBlStatusToStatus(t));
        }
        public async Task<BlStatus> GetById(int t)
        {
            return Converts.ConvertFromStatusToBlStatus(dal.Status.GetById(t).Result);
        }
    }
}
