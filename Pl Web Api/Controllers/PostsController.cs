using Bl.Api;
using Bl.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        IBl bl;
        public PostsController(IBl bl)
        {
            this.bl = bl;
        }
        [HttpGet]
        public async Task<List<BlPosts>> Get()
        {
            return await bl.Posts.GetAll();
        }

        [HttpPost]
        public async Task<bool> AddPost([FromBody] BlPosts value)
        {
            return await bl.Posts.Create(value);
        }
       
        [HttpPost]
        public async Task<bool> update([FromBody] BlPosts value)
        {
            return await bl.Posts.Update(value);
        }
        [HttpPost]
        public async Task<bool> Delete([FromBody] BlPosts value)
        {
            return await bl.Posts.Delete(value);
        }
        //[HttpPost]
        //public void ConfirmPost([FromBody] int id)
        //{
        //    bl.Posts.ConfirmPost(id);
        //}

    }
}
