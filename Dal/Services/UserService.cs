using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class UserService : IUsers
    {
        DbManager dbm;
        public UserService(DbManager dbm)
        {
            this.dbm = dbm;
        }
        public async Task<User> Create(User t)
        {
            if (t == null)
                throw new ArgumentNullException("test");
            if (t.Id == null)
                throw new Exception("id can't be null");
       
                dbm.Users.Add(t);
                try { await dbm.SaveChangesAsync(); }
                catch
                {
                    dbm.Users.Local.Remove(t);
                }
            
           
            return t;
        }

        public Task<bool> Delete(User t)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAll()
        {
            return await dbm.Users.ToListAsync();
        }

        public Task<User> GetById(int t)
        {
            throw new NotImplementedException();
        }
        public async Task<User> GetByPassword(string t)
        {
            return dbm.Users.ToList().Find(x  => x.Password == t)?? throw new Exception("The customer isnt exist!!");
        }
        public Task<bool> Update(User t)
        {
            throw new NotImplementedException();
        }
        public async Task< User> GetUserByName(string userName)
        {
            return dbm.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public Task<User> GetByPassword(int id)
        {
            throw new NotImplementedException();
        }
    }
}
