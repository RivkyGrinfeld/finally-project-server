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
    public class RequestService:IRequests
    {
        DbManager dbm;
        public RequestService(DbManager dbm)
        {
            this.dbm = dbm;
        }
        public async Task<bool> Create(RequestsTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("request");
            if (t.Id == null)
                throw new Exception("id can't be null");
            try
            {
                dbm.RequestsTbls.Add(t);
                dbm.SaveChangesAsync();
            }
            catch
            {
                dbm.RequestsTbls.Local.Remove(t);
            }
            return true;
        }
        public async Task<bool> Delete(RequestsTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("request");
            dbm.RequestsTbls.Remove(t);
            await dbm.SaveChangesAsync();
            return true;
        }
        public async Task<RequestsTbl> Get(int id)
        {
            return dbm.RequestsTbls.ToList().Where(c => c.Id.Equals(id)).FirstOrDefault();
        }
        public async Task<List<RequestsTbl>> GetAll()
        {
            return  await dbm.RequestsTbls.Include(x => x.Property).ToListAsync() ?? new List<RequestsTbl>();
        }
        public async Task<RequestsTbl> GetById(int t)
        {
            return dbm.RequestsTbls.Include(x => x.Property).ToList().Find(x => x.Id == t) ?? throw new Exception("element not found!!!");
        }
        public async Task<bool> Update(RequestsTbl t)
        {
            RequestsTbl c = dbm.RequestsTbls.ToList().Where(c => c.Id.Equals(t.Id)).FirstOrDefault();
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

