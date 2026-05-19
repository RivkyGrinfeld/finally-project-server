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
    public class BlPropertiesService:IBLProperties
    {
        IDal dal;
        public BlPropertiesService(IDal dal)
        {
            this.dal = dal;
        }

        public async Task<bool> Create(BlProperties t)
        {
            //var b = m.Map<CustomersTbl>(t);

            return await dal.Properties.Create(Converts.ConvertFromBlPropertiesToProperties(t));

        }
        public async Task<bool> Delete(BlProperties t)
        {
           return await dal.Properties.Delete(Converts.ConvertFromBlPropertiesToProperties(t));
        }
        public async Task<BlProperties> Get(int id)
        {
            return Converts.ConvertFromPropertiesToBlPropeties(dal.Properties.Get(id).Result);
        }
        public async Task<List<BlProperties>> GetAll()
        {
            List<BlProperties> blList = new List<BlProperties>();
             dal.Properties.GetAll().Result.ForEach(c =>  blList.Add(Converts.ConvertFromPropertiesToBlPropeties(c)));
            return blList;
        }
        public async Task<BlProperties> GetById(int t)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Update(BlProperties t)
        {
           return await dal.Properties.Update(Converts.ConvertFromBlPropertiesToProperties(t));
        }
    }
}
