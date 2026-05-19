using Bl.Api;
using Bl.Models;
using Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IBl _bl;

        public UserController(IBl bl)
        {
            _bl = bl;
        }
        [HttpPost]
        public async Task<BlUser> GetByPassword([FromBody] int value)
        {
            return await _bl.Users.GetByPassword(value);
        }
        [HttpPost]
        public async Task<int> CheckAuth([FromBody] LoginDto value)
        {
            return await _bl.Users.CheckAuth(value);
        }

        [HttpGet]
        public async Task<List<BlUser>> GetAll()
        {
            return await _bl.Users.GetAll();
        }

    }
}
