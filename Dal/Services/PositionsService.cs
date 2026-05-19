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
    public class PositionsService : IPositions
    {
        DbManager dbm;
        public PositionsService(DbManager dbm)
        {
            this.dbm = dbm;
        }
        public async Task<bool> Create(PositionsTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("position");
            if (t.Id == null)
                throw new Exception("id can't be null");
            try
            {
                dbm.PositionsTbls.Add(t);
                await dbm.SaveChangesAsync();
            }
            catch
            {
                dbm.PositionsTbls.Local.Remove(t);
            }
            return true;
        }
        public async Task<bool> Delete(PositionsTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("position");
            dbm.PositionsTbls.Remove(t);
            await dbm.SaveChangesAsync();
            return true;
        }
        public async Task<PositionsTbl> Get(int id)
        {
            return dbm.PositionsTbls.ToList().Where(c => c.Id.Equals(id)).FirstOrDefault();
        }
        public async Task<List<PositionsTbl>> GetAll()
        {
            return dbm.PositionsTbls.ToList();
        }
        public async Task<PositionsTbl> GetById(int t)
        {
           return dbm.PositionsTbls.ToList().Find(x => x.Id == t);
        }
        public async Task<bool> Update(PositionsTbl t)
        {
            PositionsTbl c = dbm.PositionsTbls.ToList().Where(c => c.Id.Equals(t.Id)).FirstOrDefault();
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

