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
    public class BlUserService : IBlUser
    {
        IDal dal;
        public BlUserService(IDal dal)
        {
            this.dal = dal;

        }

        public async Task<int> CheckAuth(LoginDto login)
        {
            List<BlUser> users = new();
            dal.Users.GetAll().Result.ForEach(x => users.Add(Converts.ConvertFromUserToBlUser(x)));
            BlUser u = users.Find(x => x.Password == login.Password && x.UserName == login.UserName);
            if (u == null)
            {
                throw new Exception("not valid");
            }
            if (u.StatusId == 1)
            {
                return dal.Managers.GetAll().Result.Find(x => x.UserId == u.Id).UserId;
            }
            else if (u.StatusId == 2)
            {
                return dal.Companies.GetAll().Result.Find(x => x.UserId == u.Id).UserId;
            }
            LoginDto loginDto = new LoginDto();
           
             return dal.Customers.GetAll().Result.Find(x => x.UserId == u.Id).UserId;
             

        }

        public async Task<BlUser> Create(BlUser t)
        {
            return (Converts.ConvertFromUserToBlUser(dal.Users.Create(Converts.ConvertFromBlUserToUser(t)).Result));
        }

        public Task<bool> Delete(BlUser t)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BlUser>> GetAll()
        {
            List<BlUser> blA = new List<BlUser>();
            dal.Users.GetAll().Result.ForEach(x => blA.Add(Converts.ConvertFromUserToBlUser(x)));
            return blA;
        }

        public Task<BlUser> GetById(int t)
        {
            throw new NotImplementedException();
        }
        public async Task<BlUser> GetByPassword(int t)
        {
            return Converts.ConvertFromUserToBlUser(dal.Users.GetByPassword(t).Result);
        }


        public Task<bool> Update(BlUser t)
        {
            throw new NotImplementedException();
        }
    }
}
