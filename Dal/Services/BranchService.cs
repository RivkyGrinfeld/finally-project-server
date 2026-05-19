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
    public class BranchService : IBranches
    {
        DbManager dbm;
        public BranchService(DbManager dbm)
        {
            this.dbm = dbm;
        }

        public async Task<bool> Create(BranchesTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("branch");
            if (t.Id == null)
                throw new Exception("id can't be null");
            try
            {
                dbm.BranchesTbls.Add(t);
                await dbm.SaveChangesAsync();
            }
            catch
            {
                dbm.BranchesTbls.Local.Remove(t);
            }
            return true;
        }
        public async Task<bool> Delete(BranchesTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("branch");
            dbm.BranchesTbls.Remove(t);
            await dbm.SaveChangesAsync();
            return true;
        }
        public async Task<BranchesTbl> Get(int id)
        {
            return dbm.BranchesTbls.ToList().Where(c => c.Id.Equals(id)).FirstOrDefault();
        }
        public async Task<List<BranchesTbl>> GetAll()
        {
            return await dbm.BranchesTbls.Include(x => x.PositionsTbls).ToListAsync() ?? new List<BranchesTbl>();
        }
        public async Task<BranchesTbl> GetById(int t)
        {
            return dbm.BranchesTbls.Include(x => x.PositionsTbls).ToList().Find(x => x.Id == t) ?? throw new Exception("element not found!!!");
        }
        public async Task<bool> Update(BranchesTbl t)
        {
            BranchesTbl c = dbm.BranchesTbls.ToList().Where(c => c.Id.Equals(t.Id)).FirstOrDefault();
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
    }
}

