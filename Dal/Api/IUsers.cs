using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Models;
namespace Dal.Api
{
    public interface IUsers 
    {
        public Task<User> GetByPassword(int id);
        public Task<User> GetUserByName(string userName);
        public Task<User> Create(User t);
        public Task<bool> Update(User t);
        public Task<bool> Delete(User t);
        public Task<List<User>> GetAll();
        public Task<User> GetById(int t);
    }
}
