using Bl.Api;
using Bl.Models;
using Microsoft.AspNetCore.Mvc;

 //For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        IBl bl;
        public BranchesController(IBl bl)
        {
            this.bl = bl;
        }
        [HttpPost]
        public async Task<bool> AddBranch([FromBody] BlBranches value)
        {
            return await bl.Branches.Create(value);
        }
        [HttpGet]
        public async Task<List<BlBranches>>  GetAll()
        {
            
            return await bl.Branches.GetAll();
        }
      
        [HttpGet("{id}")]
        public async Task<BlBranches> GetById(int id)
        {
            return await bl.Branches.GetById(id);
        }
    }
}
