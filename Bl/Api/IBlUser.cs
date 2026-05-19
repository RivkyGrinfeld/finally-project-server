using Bl.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    public interface IBlUser
    {
        public Task<BlUser> GetByPassword(int id);
        public Task<int> CheckAuth(LoginDto login);
        public Task<BlUser> Create(BlUser t);
        public Task<bool> Update(BlUser t);
        public Task<bool> Delete(BlUser t);
        public Task<List<BlUser>> GetAll();
        public Task<BlUser> GetById(int t);

    }
}
