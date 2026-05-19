using Bl.Api;
using Bl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        IBl bl;
        public ManagerController(IBl bl)
        {
            this.bl = bl;
        }
        [HttpGet]
        public async Task<List<BlManagers>> GetAll()
        {
            return await bl.Managers.GetAll();
        }

        //[HttpPost]
        //public async Task<bool> AddPost([FromBody] BlManagers value)
        //{
        //    return await bl.Posts.Create(value);
        //}

        //[HttpPost]
        //public async Task<bool> update([FromBody] BlPosts value)
        //{
        //    return await bl.Posts.Update(value);
        //}
        //[HttpPost]
        //public async Task<bool> Delete([FromBody] BlPosts value)
        //{
        //    return await bl.Posts.Delete(value);
        //}
        ////[HttpPost]
        //public void ConfirmPost([FromBody] int id)
        //{
        //    bl.Posts.ConfirmPost(id);
        //}

    }
}

