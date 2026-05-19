using Bl.Api;
using Bl.Models;
using Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        IBl bl;
        public TestController(IBl bl)
        {
            this.bl = bl;
        }
        [HttpPost]
        public async Task<bool> AddTest([FromBody]BlTest  value)
        { 
            return await bl.Test.Create(value);
        }
        [HttpGet]
        public async Task<List<BlTest>> GetAll()
        {

            return await bl.Test.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<BlTest> GetById(int id)
        {
            return await bl.Test.GetById(id);
        }
    }
}
