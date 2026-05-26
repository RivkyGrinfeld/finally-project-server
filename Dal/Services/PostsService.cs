//using Dal.Api;
//using Dal.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Dal.Services
//{
//    public class PostsService : IPosts
//    {
//        DbManager dbManager;
//        public PostsService(DbManager dbManager)
//        {
//            this.dbManager = dbManager;
//        }

//        public void ConfirmPost(int id)
//        {
//            dbManager.PostsTbls.ToList().FindAll(v =>v.Id == id).FirstOrDefault().IsConfirmed = true;
//            dbManager.SaveChangesAsync();
//        }
//        public async Task<bool> Create(PostsTbl t)
//        {
//            if (t == null)
//                throw new ArgumentNullException("post");
//            if (t.Id == null)
//                throw new Exception("id can't be null");
//            //t.RequestsTbls.ToList().ForEach(v =>  v.PostId = t.Id);
//            dbManager.PostsTbls.Add(t);
//            await dbManager.SaveChangesAsync();
//            return true;
//        }
//        public async Task<bool> Delete(PostsTbl t)
//        {
//            if (t == null)
//                throw new ArgumentNullException("post");
//            dbManager.PostsTbls.Remove(t);
//            await dbManager.SaveChangesAsync();
//            return true;
//        }
//        public async Task<PostsTbl> Get(int id)
//        {
//            return dbManager.PostsTbls.Include(x => x.RequestsTbls).ToList().Where(c => c.Id.Equals(id)).FirstOrDefault();
//        }
//        public async Task<List<PostsTbl>> GetAll()
//        {
//            return await dbManager.PostsTbls.Include(x => x.RequestsTbls).ToListAsync() ?? new List<PostsTbl>();
//        }
//        public async Task<PostsTbl> GetById(int t)
//        {
//            throw new NotImplementedException();
//        }
//        public async Task<bool> Update(PostsTbl t)
//        {
//            PostsTbl c = dbManager.PostsTbls.ToList().Where(c => c.Id.Equals(t.Id)).FirstOrDefault();



//            //PostsTbl c = dbManager.Posts.ToList().Where(c => c.Id.Equals(val.Id)).FirstOrDefault();
//            if (c == null)
//                return false;
//            //c.Id = t.Id;
//            c.City = t.City;
//            c.IsConfirmed = t.IsConfirmed;
//            c.Date = t.Date;
//            c.IsAvailble = t.IsAvailble;
//            c.Salary = t.Salary;
//            c.CompanyId = t.CompanyId;

//            dbManager.PostsTbls.Update(c);
//            dbManager.SaveChangesAsync();

//            return true;
//        }




//        //var source = t.GetType().GetProperties();
//        //    var dest = c.GetType().GetProperties();
//        //    foreach (var item in source)
//        //    {
//        //        var destProperty = dest.FirstOrDefault(p => p.Name == item.Name && p.CanWrite);
//        //        if (destProperty != null)
//        //        {
//        //            var val = item.GetValue(t);
//        //            destProperty.SetValue(c, val);
//        //        }
//        //    }
//        //    return true;
//        //}
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
    public class PostsService:IPosts
    {
        private readonly DbManager _dbManager;

        public PostsService(DbManager dbManager)
        {
            _dbManager = dbManager;
        }

        // שליפת משרה לפי ID
        public async Task<PostsTbl> Get(int id)
        {
            return await _dbManager.PostsTbls
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // שליפת כל המשרות
        public async Task<List<PostsTbl>> GetAll()
        {
            return await _dbManager.PostsTbls
                .Include(p => p.RequestsTbls)
                .ToListAsync();
            //return await _dbManager.PostsTbls.ToListAsync();
        }
        public async Task<PostsTbl> GetById(int t)
        {
            throw new NotImplementedException();
        }
        // יצירת משרה
        public async Task<bool> Create(PostsTbl post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _dbManager.PostsTbls.Add(post);
            await _dbManager.SaveChangesAsync();
            return true;
        }

        // עדכון משרה
        public async Task<bool> Update(PostsTbl post)
        {
            var existingPost = await _dbManager.PostsTbls
                .FirstOrDefaultAsync(p => p.Id == post.Id);

            if (existingPost == null)
                return false;

            existingPost.City = post.City;
            existingPost.IsConfirmed = post.IsConfirmed;
            existingPost.Date = post.Date;
            existingPost.Salary = post.Salary;
            existingPost.CompanyId = post.CompanyId;
            existingPost.PositionId = post.PositionId;
            existingPost.Position = post.Position;
            existingPost.IsAvailble = post.IsAvailble;
            existingPost.JobDescription = post.JobDescription;
            existingPost.MaxCadidated = post.MaxCadidated;

            _dbManager.PostsTbls.Update(existingPost);
            await _dbManager.SaveChangesAsync();
            return true;
        }

        // מחיקת משרה
        public async Task<bool> Delete(PostsTbl post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _dbManager.PostsTbls.Remove(post);
            await _dbManager.SaveChangesAsync();
            return true;
        }
        //public void ConfirmPost(int id)
        //{
        //    _dbManager.PostsTbls.ToList().FindAll(v => v.Id == id).FirstOrDefault().IsConfirmed = true;
        //    _dbManager.SaveChangesAsync();
        //}
        public async Task ConfirmPost(int id)
        {
            var post = await _dbManager.PostsTbls.FirstOrDefaultAsync(v => v.Id == id);
            if (post != null)
            {
                post.IsConfirmed = true;
                await _dbManager.SaveChangesAsync();
            }
        }
    }
}
