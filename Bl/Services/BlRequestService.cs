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
    public class BlRequestService:IBlRequest
    {
        IDal dal;
        public BlRequestService(IDal dal)
        {
            this.dal = dal;
        }

        public async Task<bool> Create(BlRequest t)
        {
            RequestsTbl r  = Converts.ConvertFromBlRequestToRequest(t);
            r.Property = dal.Properties.GetById(t.PropertyId).Result;
            
            return await dal.Requests.Create(r);
        }
        public async Task<bool> Delete(BlRequest t)
        {
           return await dal.Requests.Delete(Converts.ConvertFromBlRequestToRequest(t));         
        }
        public async Task<BlRequest> Get(int id)
        {
            return Converts.ConvertFromRequestToBlRequest(dal.Requests.Get(id).Result);
        }
        public async Task<List<BlRequest>> GetAll()
        {
            List<BlRequest> blList = new List<BlRequest>();

            dal.Requests.GetAll().Result.ForEach(c => blList.Add(Converts.ConvertFromRequestToBlRequest(c)));
            return blList;
        }
        public async Task<BlRequest> GetById(int t)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Update(BlRequest t)
        {
           return await dal.Requests.Update(Converts.ConvertFromBlRequestToRequest(t));
        }
    }
}

