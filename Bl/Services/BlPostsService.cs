using Bl.Api;
using Bl.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class BlPostsService : IBlPosts
    {
        IDal dal;
        public BlPostsService(IDal dal)
        {
            this.dal = dal;
        }
     
        public async Task<bool> Create(BlPosts t)
        {
            //t.Requests.ForEach(request => request.PostId = t.Id);
            PostsTbl p = Converts.ConvertFromBlPostToPost(t);
            p.Company = dal.Companies.GetById(t.CompanyId).Result;
            p.Position = dal.Positions.GetById(t.PositionId).Result;
            p.RequestsTbls.ToList().ForEach(x => x.Property = dal.Properties.GetById(x.PropertyId).Result);
            //p.RequestsTbls.ToList().ForEach(x => x. = dal.Properties.GetById(x.PropertyId).Result);
            return dal.Posts.Create(p).Result;
        }
        public async Task<bool> Delete(BlPosts t)
        {
           return await dal.Posts.Delete(Converts.ConvertFromBlPostToPost(t));
        }
        public async Task<BlPosts> Get(int id)
        {
            return Converts.ConvertFromPostToBlPost(dal.Posts.Get(id).Result);
        }
        public async Task<List<BlPosts>> GetAll()
        {
            var posts = await dal.Posts.GetAll();
            List<BlPosts> blList = new List<BlPosts>();

            posts.ForEach(c => blList.Add(Converts.ConvertFromPostToBlPost(c)));

            return blList;
        }
        public async Task<bool> Update(BlPosts t)
        {
            return await dal.Posts.Update(Converts.ConvertFromBlPostToPost(t));
        }     
        public void ConfirmPost(int id)
        {
            dal.Posts.ConfirmPost(id);
        }
        public async Task<BlPosts> GetById(int t)
        {
            throw new NotImplementedException();
        }





    }




}
