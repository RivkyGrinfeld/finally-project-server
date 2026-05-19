using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class CustomerService : ICustomer
    {
        DbManager dbm;
        public CustomerService(DbManager dbm)
        {
            this.dbm = dbm;
        }

        public async Task<bool> Create(CustomersTbl t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            if (t.Id == null)
                throw new Exception("id can't be null");

            try
            {
                var trackedEntity = dbm.CustomersTbls.Local.FirstOrDefault(x => x.Id == t.Id);
                if (trackedEntity != null)
                {
                    // אפשר לשחרר את המעקב או להחליף את ה-instance לפי הצורך
                    dbm.Entry(trackedEntity).State = EntityState.Detached;
                }

                dbm.CustomersTbls.Add(t);
                await dbm.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה אם צריך
                return false;
            }
        }


        //public async Task<bool> Create(CustomersTbl t)
        //{
        //    if (t == null)
        //        throw new ArgumentNullException("customer");
        //    if (t.Id == null)
        //        throw new Exception("id can't be null");
        //    try
        //    {
        //        dbm.CustomersTbls.Add(t);
        //        await dbm.SaveChangesAsync();
        //    }
        //    catch {
        //        dbm.CustomersTbls.Local.Remove(t);
        //    }
        //    return true;
        //}
        public async Task<bool> Delete(CustomersTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("customer");
            dbm.CustomersTbls.Remove(t);
            await dbm.SaveChangesAsync();
            return true;
        }
        public async Task<CustomersTbl> Get(string id)
        {
            return  dbm.CustomersTbls.ToList().Where(c => c.Id.Equals(id)).FirstOrDefault();
        }
        public async Task<List<CustomersTbl>> GetAll()
        {

            return dbm.CustomersTbls.Include(x => x.TestsTbls).Include(x => x.ApplyTbls).ToList() ?? new List<CustomersTbl>();
        }
        public async Task<CustomersTbl> GetById(int t)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Update(CustomersTbl t)
        {
            CustomersTbl c = dbm.CustomersTbls.ToList().Where(c => c.Id.Equals(t.Id)).FirstOrDefault();
            if (c == null)
                return false;
         
          var vv=  t.GetType().GetProperties();
            var cc=  c.GetType().GetProperties();
            foreach (var item in vv)
            {
                var destProperty = cc.FirstOrDefault(p => p.Name == item.Name && p.CanWrite);
                if (destProperty != null)
                {
                    var val = item.GetValue(t);
                    destProperty.SetValue(c, val);
                }
            }
            //c.Id = t.Id;
            //c.Address = t.Address;
            //c.City = t.City;
            //c.Email = t.Email;
            //c.BornDate = t.BornDate;
            //c.FirstName = t.FirstName;
            //c.LastName = t.LastName;
            //c.NumOfChildren = t.NumOfChildren;       
            //c.Status = t.Status;

                    
            return true;
        }
        public async Task<CustomersTbl> GetByUserId(int userId)
        {
            return dbm.CustomersTbls.ToListAsync().Result.Find(x => x.UserId == userId);
        }
    }

}
