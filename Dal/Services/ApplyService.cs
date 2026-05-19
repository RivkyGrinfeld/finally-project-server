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
    public class ApplyService : IApply
    {
        DbManager dbm;
        public ApplyService(DbManager dbm)
        {
            this.dbm = dbm;
        }
        public async Task<bool> Create(ApplyTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("apply");
            if (t.Id == null)
                throw new Exception("id can't be null");
            try
            {
                dbm.ApplyTbls.Add(t);
                await dbm.SaveChangesAsync();
            }
            catch
            {
                dbm.ApplyTbls.Local.Remove(t);
            }
            return true;
        }

        public Task<bool> Delete(ApplyTbl t)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ApplyTbl>> GetAll()
        {
            return dbm.ApplyTbls.ToList() ?? new List<ApplyTbl>();
        }

        public Task<ApplyTbl> GetById(int t)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ApplyTbl t)
        {
            var c = await dbm.ApplyTbls
                .FirstOrDefaultAsync(x => x.Id == t.Id);

            if (c == null)
                return false;

            c.Cust = t.Cust;
            c.CustId = t.CustId;
            c.Date = t.Date;
            c.Confirmed = t.Confirmed;
            c.Post = t.Post;
            dbm.ApplyTbls.Update(c);
            await dbm.SaveChangesAsync();

            return true;
        }
    }
}
