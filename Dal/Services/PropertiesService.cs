//using Dal.Api;
//using Dal.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Dal.Services
//{
//    public class PropertiesService:IProperties
//    {
//        DbManager dbm;
//        public PropertiesService(DbManager dbm)
//        {
//            this.dbm = dbm;
//        }
//        public async Task<bool> Create(PropertiesTbl t)
//        {
//            if (t == null)
//                throw new ArgumentNullException("property");
//            if (t.Id == null)
//                throw new Exception("id can't be null");
//            try
//            {
//                dbm.PropertiesTbls.Add(t);
//                await dbm.SaveChangesAsync();
//            }
//            catch
//            {
//                dbm.PropertiesTbls.Local.Remove(t);
//            }
//            return true;
//        }
//        public async Task<bool> Delete(PropertiesTbl t)
//        {
//            if (t == null)
//                throw new ArgumentNullException("property");
//            dbm.PropertiesTbls.Remove(t);
//            await dbm.SaveChangesAsync();
//            return true;
//        }
//        public async Task<PropertiesTbl> Get(int id)
//        {
//            return dbm.PropertiesTbls.ToList().Where(c => c.Id.Equals(id)).FirstOrDefault();
//        }
//        public async Task<List<PropertiesTbl>> GetAll()
//        {
//            return dbm.PropertiesTbls.ToList() ?? new List<PropertiesTbl>();
//        }
//        public async Task<PropertiesTbl> GetById(int t)
//        {
//            return dbm.PropertiesTbls.ToList().Find(x => x.Id == t);
//        }
//        public async Task<bool> Update(PropertiesTbl t)
//        {
//            PropertiesTbl c = dbm.PropertiesTbls.ToList().Where(c => c.Id.Equals(t.Id)).FirstOrDefault();
//            if (c == null)
//                return false;

//            var vv = t.GetType().GetProperties();
//            var cc = c.GetType().GetProperties();
//            foreach (var item in vv)
//            {
//                var destProperty = cc.FirstOrDefault(p => p.Name == item.Name && p.CanWrite);
//                if (destProperty != null)
//                {
//                    var val = item.GetValue(t);
//                    destProperty.SetValue(c, val);
//                }
//            }
//            return true;
//        }
//    }
//}
using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class PropertiesService:IProperties
    {
        private readonly DbManager _dbManager;

        public PropertiesService(DbManager dbManager)
        {
            _dbManager = dbManager;
        }

        // שליפת מאפיין לפי ID
        public async Task<PropertiesTbl> Get(int id)
        {
            return await _dbManager.PropertiesTbls
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // שליפת כל המאפיינים
        public async Task<List<PropertiesTbl>> GetAll()
        {
            return await _dbManager.PropertiesTbls.ToListAsync();
        }

        // יצירת מאפיין
        public async Task<bool> Create(PropertiesTbl property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            _dbManager.PropertiesTbls.Add(property);
            await _dbManager.SaveChangesAsync();
            return true;
        }

        // עדכון מאפיין
        public async Task<bool> Update(PropertiesTbl property)
        {
            var existingProperty = await _dbManager.PropertiesTbls
                .FirstOrDefaultAsync(p => p.Id == property.Id);

            if (existingProperty == null)
                return false;

            existingProperty.Description = property.Description;
            _dbManager.PropertiesTbls.Update(existingProperty);
            await _dbManager.SaveChangesAsync();
            return true;
        }

        // מחיקת מאפיין
        public async Task<bool> Delete(PropertiesTbl property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            _dbManager.PropertiesTbls.Remove(property);
            await _dbManager.SaveChangesAsync();
            return true;
        }

        public async Task<PropertiesTbl> GetById(int t)
        {
            return _dbManager.PropertiesTbls.ToList().Find(x => x.Id == t);
        }
    }
}