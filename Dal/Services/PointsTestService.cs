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
    public class PointsTestService : IPointsTest
    {
        DbManager dbm;
        public PointsTestService(DbManager dbm)
        {
            this.dbm = dbm;
        }

        public async Task<bool> Create(PointsTestTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("pointTest");
            if (t.Id == null)
                throw new Exception("id can't be null");
            try
            {
                dbm.PointsTestTbls.Add(t);
                await dbm.SaveChangesAsync();
            }
            catch
            {
                dbm.PointsTestTbls.Local.Remove(t);
            }
            return true;
        }
        public async Task<bool> Delete(PointsTestTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("pointTest");
            dbm.PointsTestTbls.Remove(t);
            await dbm.SaveChangesAsync();
            return true;
        }
        public async Task<PointsTestTbl> Get(int id)
        {
            return dbm.PointsTestTbls.ToList().Where(c => c.Id.Equals(id)).FirstOrDefault();
        }
        public async Task<List<PointsTestTbl>> GetAll()
        {
            return await dbm.PointsTestTbls.ToListAsync() ?? new List<PointsTestTbl>();
        }
        public async Task<PointsTestTbl> GetById(int t)
        {
            return dbm.PointsTestTbls.ToList().Find(x => x.Id == t) ?? throw new Exception("element not found!!!");
        }
        public async Task<bool> Update(PointsTestTbl t)
        {

            var existingPost = await dbm.PointsTestTbls
                .FirstOrDefaultAsync(p => p.Id == t.Id);

            if (existingPost == null)
                return false;

            existingPost.TestId = t.TestId;
            //existingPost.Property = t.Property;
            //existingPost.Test = t.Test;
            existingPost.PropertyId = t.PropertyId;
            existingPost.GradeProperty = t.GradeProperty;
            dbm.PointsTestTbls.Update(existingPost);
            try
            {
                await dbm.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
