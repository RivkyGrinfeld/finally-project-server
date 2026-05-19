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
    public class BlCompaniesService : IBlCompanies
    {
        IDal dal;
        public BlCompaniesService(IDal dal)
        {
            this.dal = dal;
        }
      
        public async Task<bool> Create(BlCompanies t)
        {

          return await dal.Companies.Create(Converts.ConvertFromBlCompaniesToCompanies(t));
          
        }
        public async Task<bool> Delete(BlCompanies t)
        {
           return await dal.Companies.Delete(Converts.ConvertFromBlCompaniesToCompanies(t));
        }
        public async Task<BlCompanies> Get(int id)
        {
            return Converts.ConvertFromCompaniesToBlCompanies(dal.Companies.Get(id).Result);
        }
        public async Task<List<BlCompanies>> GetAll()
        {
            List<BlCompanies> blList = new List<BlCompanies>();
            dal.Companies.GetAll().Result.ForEach(c => blList.Add(Converts.ConvertFromCompaniesToBlCompanies(c)));
            return blList;
        }
        public async Task<bool> Update(BlCompanies t)
        {
           return await dal.Companies.Update(Converts.ConvertFromBlCompaniesToCompanies(t));          
        }
        public async Task<List<BlPosts>> GetMyPosts(int id)
        {           
            List<BlPosts> n = new List<BlPosts>();
             dal.Companies.Get(id).Result.PostsTbls.ToList()
                    .ForEach(x => n.Add(Converts.ConvertFromPostToBlPost(x)));
            return n;
        }
        public async Task<BlCompanies> GetById(int t)
        {
            throw new NotImplementedException();
        }

        public async Task<BlCompanies> GetByUserId(int userId)
        {
            return Converts.ConvertFromCompaniesToBlCompanies( dal.Companies.GetByUserId(userId).Result);
        }
    }
}
