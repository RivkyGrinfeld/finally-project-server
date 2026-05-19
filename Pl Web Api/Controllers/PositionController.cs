using Bl.Api;
using Bl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        IBl bl;
        public PositionController(IBl bl)
        {
            this.bl = bl;
        }
        [HttpPost]
        public async Task<bool> AddPosition([FromBody] BlPositions value)
        {
            return await bl.Positions.Create(value);
        }
        [HttpGet]
        public async Task<List<BlPositions>> GetAll()
        {
            return await bl.Positions.GetAll();
        }
        [HttpPost]
        public List<BlPositions> GetAllPositionByBranch([FromBody] int id)
        {
            return bl.Branches.GetById(id).Result.PositionsTbls.ToList();
        }
    }
}
