using Bl.Api;
using Bl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplyController : ControllerBase
    {
        IBl bl;
        public ApplyController(IBl bl)
        {
            this.bl = bl;
        }
        [HttpPost]
        public async Task<bool> AddApply([FromBody] BlApply value)
        {
            return await bl.Apply.Create(value);
        }
        [HttpGet]
        public async Task<List<BlApply>> GetAll()
        {

            return await bl.Apply.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<BlApply> GetById(int id)
        {
            return await bl.Apply.GetById(id);
        }
        [HttpPost]
        public async Task<bool> Update([FromBody] BlApply value)
        {
            return await bl.Apply.Update(value);
        }
    }
}
