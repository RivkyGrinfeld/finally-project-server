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
    public class BlPointsTestService: IBlPointsTest
    {
        IDal dal;
        public BlPointsTestService(IDal dal)
        {
            this.dal = dal;

        }
        public async Task<bool> Create(BlPointsTest t)
        {
            //t.Property = Converts.ConvertFromPropertiesToBlPropeties( dal.Properties.Get(t.PropertyId).Result);
            return await dal.PointsTests.Create(Converts.ConvertFromBlPointsTestToPointsTest(t));

        }
        public async Task<bool> Delete(BlPointsTest t)
        {
            return await dal.PointsTests.Delete(Converts.ConvertFromBlPointsTestToPointsTest(t));
        }
        public async Task<BlPointsTest> Get(int id)
        {
            return Converts.ConvertFromPointsTestToBlPointsTest(dal.PointsTests.Get(id).Result);
        }
        public async Task<List<BlPointsTest>> GetAll()
        {
            List<BlPointsTest> blList = new List<BlPointsTest>();
            dal.PointsTests.GetAll().Result.ForEach(c => blList.Add(Converts.ConvertFromPointsTestToBlPointsTest(c)));
            return blList;
        }
        public async Task<bool> Update(BlPointsTest t)
        {
            return await dal.PointsTests.Update(Converts.ConvertFromBlPointsTestToPointsTest(t));
        }
        public async Task<BlPointsTest> GetById(int t)
        {
            return Converts.ConvertFromPointsTestToBlPointsTest(dal.PointsTests.GetById(t).Result);
        }
    }
}
