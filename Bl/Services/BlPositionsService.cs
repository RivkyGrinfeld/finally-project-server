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
    public class BlPositionsService:IBlPositions
    {
        IDal dal;
        public BlPositionsService(IDal dal)
        {
            this.dal = dal;

        }

        public async Task<bool> Create(BlPositions t)
        {
            //var b = m.Map<CustomersTbl>(t);

            return await dal.Positions.Create(Converts.ConvertFromBlPositionToPosition(t));

        }
        public async Task<bool> Delete(BlPositions t)
        {
           return await dal.Positions.Delete(Converts.ConvertFromBlPositionToPosition(t));
        }
        public async Task<BlPositions> Get(int id)
        {
            return Converts.ConvertFromPositionToBlPosition(dal.Positions.Get(id).Result);
        }
        public async Task<List<BlPositions>> GetAll()
        {
            List<BlPositions> blList = new List<BlPositions>();
            dal.Positions.GetAll().Result.ForEach(c => blList.Add(Converts.ConvertFromPositionToBlPosition(c)));
            return blList;
        }
        public async Task<BlPositions> GetById(int t)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Update(BlPositions t)
        {
           return await dal.Positions.Update(Converts.ConvertFromBlPositionToPosition(t));
        }
    }
}
