using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class StatusService: IStatus
    {
        DbManager dbm;
        public StatusService(DbManager dbm)
        {
            this.dbm = dbm;
        }

        public async Task<bool> Create(StatusTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("status");
            if (t.Id == null)
                throw new Exception("id can't be null");
            try
            {
                dbm.StatusTbls.Add(t);
                await dbm.SaveChangesAsync();
            }
            catch
            {
                dbm.StatusTbls.Local.Remove(t);
            }
            return true;
        }
        public async Task<bool> Delete(StatusTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("status");
            dbm.StatusTbls.Remove(t);
            await dbm.SaveChangesAsync();
            return true;
        }
        public async Task<StatusTbl> Get(int id)
        {
            return dbm.StatusTbls.ToList().Where(c => c.Id.Equals(id)).FirstOrDefault();
        }
        public async Task<List<StatusTbl>> GetAll()
        {
            return dbm.StatusTbls.ToList() ?? new List<StatusTbl>();
        }
        public async Task<StatusTbl> GetById(int t)
        {
            return dbm.StatusTbls.ToList().Find(x => x.Id == t) ?? throw new Exception("element not found!!!");
        }
        public async Task<bool> Update(StatusTbl t)
        {
            StatusTbl c = dbm.StatusTbls.ToList().Where(c => c.Id.Equals(t.Id)).FirstOrDefault();
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
