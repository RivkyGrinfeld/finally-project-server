using Bl.Api;
using Bl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

        
        public class RequestController : ControllerBase
        {
            IBl bl;
            public RequestController(IBl bl)
            {
                this.bl = bl;
            }
            [HttpPost]
            public async Task<bool> AddRequest([FromBody] BlRequest value)
            {
                return await bl.Request.Create(value);
            }
            [HttpGet]
            public async Task<List<BlRequest>> GetAll()
            {

                return await bl.Request.GetAll();
            }

            [HttpGet("{id}")]
            public async Task<BlRequest> GetById(int id)
            {
                return await bl.Request.GetById(id);
            }
        }
    }
