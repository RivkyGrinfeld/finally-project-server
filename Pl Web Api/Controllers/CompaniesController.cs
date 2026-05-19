using Bl.Api;
using Bl.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        IBl bl;
        public CompaniesController(IBl bl)
        {
            this.bl = bl;
        }

        [HttpPost]
        public async Task<bool> AddCompany([FromBody] BlCompanies value)
        {
            BlUser us = await bl.Users.Create(new BlUser() { Password = value.Password, UserName = value.UserName, StatusId = 2 });
            value.UserId = us.Id;
            return await bl.Companies.Create(value);
        }
        [HttpGet("{id}")]
        public async Task<List<BlPosts>> GetMyPosts(int id)
        {
            return await bl.Companies.GetMyPosts(id);
        }
        [HttpGet]
        public async Task<List<BlCompanies>> GetAll()
        {
            return await bl.Companies.GetAll();
        }
        //[HttpDelete("{id}")]
        //public async Task<bool> Delete(int id)
        //{
        //    return await bl.Companies.Delete(id);
            
        //}
    }
}
