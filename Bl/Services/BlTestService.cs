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
    public class BlTestService: IBlTest
    {
        IDal dal;
        public BlTestService(IDal dal)
        {
            this.dal = dal;

        }
        public async Task<bool> Create(BlTest t)
        {
             //t.Cust = Converts.ConvertFromCustomerToBlCustomer(await dal.Customers.Get(t.CustId));
            return await dal.Tests.Create(Converts.ConvertFromBlTestToTest(t));

        }
        public async Task<bool> Delete(BlTest t)
        {
            return await dal.Tests.Delete(Converts.ConvertFromBlTestToTest(t));

        }
        public async Task<BlTest> Get(int id)
        {
            return Converts.ConvertFromTestToBlTest(dal.Tests.Get(id).Result);
        }
        public async Task<List<BlTest>> GetAll()
        {
            List<BlTest> blList = new List<BlTest>();
            dal.Tests.GetAll().Result.ForEach(c => blList.Add(Converts.ConvertFromTestToBlTest(c)));
            return blList;
        }
        public async Task<bool> Update(BlTest t)
        {
            return await dal.Tests.Update(Converts.ConvertFromBlTestToTest(t));
        }
        public async Task<BlTest> GetById(int t)
        {
            return Converts.ConvertFromTestToBlTest(dal.Tests.GetById(t).Result);
        }
    }
}
