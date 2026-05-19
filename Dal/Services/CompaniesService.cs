using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Services
{
    public class CompaniesService : ICompanies
    {
        DbManager dbManager;
        public CompaniesService(DbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public async Task<bool> Create(CompaniesTbl t)
        {

            if (t == null)
                throw new ArgumentNullException("company");
            if (t.Id == null)
                throw new Exception("id can't be null");
            try
            {
                dbManager.CompaniesTbls.Add(t);
                await dbManager.SaveChangesAsync();
            }
            catch
            {
                dbManager.CompaniesTbls.Local.Remove(t);
                return false;
            }
            return true;
        }
        public async Task<bool> Delete(CompaniesTbl t)
        {
            if (t == null)
                throw new ArgumentNullException("company");
            dbManager.CompaniesTbls.Remove(t);
            await dbManager.SaveChangesAsync();
            return true;
        }
        public async Task<CompaniesTbl> Get(int id)
        {
            return dbManager.CompaniesTbls.Include(x => x.PostsTbls).ToList().Where(c => c.Id.Equals(id)).FirstOrDefault();
        }
        public async Task<List<CompaniesTbl>> GetAll()
        {
            return dbManager.CompaniesTbls.Include(x => x.PostsTbls).ToList() ?? new List<CompaniesTbl>();
        }
        public async Task<CompaniesTbl> GetById(int t)
        {
            return dbManager.CompaniesTbls.ToList().Find(x => x.Id == t);
        }

        public async Task<CompaniesTbl> GetByUserId(int userId)
        {
           return  dbManager.CompaniesTbls.ToListAsync().Result.Find(x=> x.UserId == userId);
        }

        public async Task<bool> Update(CompaniesTbl t)
        {
            CompaniesTbl c = dbManager.CompaniesTbls.ToList().Where(c => c.Id.Equals(t.Id)).FirstOrDefault();
            if (c == null)
                return false;

            var source = t.GetType().GetProperties();
            var dest = c.GetType().GetProperties();
            foreach (var item in source)
            {
                var destProperty = dest.FirstOrDefault(p => p.Name == item.Name && p.CanWrite);
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