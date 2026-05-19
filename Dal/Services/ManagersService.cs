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
    public class ManagersService : IManager
    {
        DbManager dbm;
        public ManagersService(DbManager dbm)
        {
            this.dbm = dbm;
        }
        public async Task<bool> Create(ManagersTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("manager");
            if (t.Id == null)
                throw new Exception("id can't be null");
            dbm.ManagersTbls.Add(t);
            await dbm.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(ManagersTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("manager");
            dbm.ManagersTbls.Remove(t);
            await dbm.SaveChangesAsync();
            return true;
        }

        public async Task<ManagersTbl> Get(int id)
        {
            return dbm.ManagersTbls.ToList().Where(c => c.Id.Equals(id)).FirstOrDefault();
        }

        public async Task<List<ManagersTbl>> GetAll()
        {
            return dbm.ManagersTbls.ToList() ?? new List<ManagersTbl>();
        }

        public async Task<ManagersTbl> GetById(int t)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ManagersTbl t)
        {
            ManagersTbl c = dbm.ManagersTbls.ToList().Where(c => c.Id.Equals(t.Id)).FirstOrDefault();
            if (c == null)
                return false;

            var vv = t.GetType().GetProperties();
            var cc = c.GetType().GetProperties();
            foreach (var item in vv)
            {
                var destProperty = cc.FirstOrDefault(p => p.Name == item.Name && p.CanWrite);
                if (destProperty != null)
                {
                    var val = item.GetValue(t);
                    destProperty.SetValue(c, val);
                }
            }
            return true;
        }
        public async Task<ManagersTbl> GetByUserId(int userId)
        {
            return dbm.ManagersTbls.ToListAsync().Result.Find(x => x.UserId == userId);
        }
    }
}
