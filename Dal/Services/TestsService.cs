using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dal.Services
{
    public class TestsService:ITest
    {
        DbManager dbm;
        public TestsService(DbManager dbm)
        {
            this.dbm = dbm;
        }
        public async Task<bool> Create(TestsTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("test");
            if (t.TestId == null)
                throw new Exception("id can't be null");
            var existingCustomer = dbm.CustomersTbls
                                 .FirstOrDefault(c => c.Id == t.CustId);

            if (existingCustomer != null)
            {
                // מנתק את הלקוח מהמעקב אם הוא כבר במעקב
                dbm.Entry(existingCustomer).State = EntityState.Detached;
            }          
            try
            {
                dbm.TestsTbls.Add(t);
                try {  await dbm.SaveChangesAsync();}
                catch
                {
                    dbm.TestsTbls.Local.Remove(t);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        //public async Task<bool> Create(TestsTbl t)
        //{
        //    if (t == null)
        //        throw new ArgumentNullException("test");

        //    // אם ה־Cust כבר במעקב, נסלק אותו מה־DbContext
        //    var custEntry = dbm.Entry(t.Cust);
        //    if (custEntry.State != EntityState.Detached)
        //    {
        //        dbm.Entry(t.Cust).State = EntityState.Detached;  // נוודא שה־Cust לא במעקב
        //    }

        //    try
        //    {
        //        dbm.TestsTbls.Add(t);  // הוסף את ה־Test
        //        await dbm.SaveChangesAsync();  // שמור את השינויים במסד נתונים
        //    }
        //    catch (Exception ex)
        //    {
        //        dbm.TestsTbls.Local.Remove(t);  // אם קרתה שגיאה, ננסה להסיר את ה־Test מהמניפולציה המקומית
        //        throw new Exception("Error adding test", ex);
        //    }

        //    return true;
        //}
        public async Task<bool> Delete(TestsTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("test");
            dbm.TestsTbls.Remove(t);
            await dbm.SaveChangesAsync();
            return true;
        }
        public async Task<TestsTbl> Get(int id)
        {
            return dbm.TestsTbls.ToList().Where(c => c.TestId.Equals(id)).FirstOrDefault();
        }
        public async Task<List<TestsTbl>> GetAll()
        {
            return dbm.TestsTbls.Include(x => x.PointsTestTbls).ToList() ?? new List<TestsTbl>();
        }
        public async Task<TestsTbl> GetById(int t)
        {
            return dbm.TestsTbls.ToList().Find(x => x.TestId == t) ?? throw new Exception("element not found!!!");
        }
        public async Task<bool> Update(TestsTbl t)
        {
            TestsTbl c = dbm.TestsTbls.ToList().Where(c => c.TestId.Equals(t.TestId)).FirstOrDefault();
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