using Bl.Api;
using Bl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        IBl bl;
        public PropertiesController(IBl bl)
        {
            this.bl = bl;
        }

        [HttpPost]
        public async Task<bool> AddProperty([FromBody] BlProperties value)
        {
            return await bl.Properties.Create(value);
        }
        [HttpGet]
        public async Task<List<BlProperties>> GetAll()
        {
            return await bl.Properties.GetAll();
        }

    }
}
