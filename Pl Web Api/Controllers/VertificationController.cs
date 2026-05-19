using Bl.Api;
using Bl.Models;
using Bl.Services;
using Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VertificationController : ControllerBase
    {
        IBl Bl;
        public VertificationController(IBl bl)
        {
          Bl = bl;
        }
        [HttpPost]
        public async Task <IActionResult> SendVerificationEmail(string id)
        {    
            string token = Guid.NewGuid().ToString();
            DateTime creationTime = DateTime.Now;
            string verificationLink = "https://localhost:4200/verify?token={token}"; // כאן תיצור את הקישור לאימות
            var verificationToken = new BlUserVertificationToken
            {
                UserId = id,
                Token = token,
                CreationTime = creationTime,
                IsVerified = false
            };
            Bl.UserVertificationTokens.Create(verificationToken);
            var emailService = new EmailService();
            string email = Bl.Customers.Get(id).Result.Email;
            emailService.SendVerificationEmail(email, verificationLink);

            return Ok("הודעת האימות נשלחה");
        }
        [HttpGet]
        public IActionResult VerifyAccount(string token)
        {
            Bl.UserVertificationTokens.GetByToken(token).ContinueWith(task =>
            {
                var verificationToken = task.Result;
                if (verificationToken != null && !verificationToken.IsVerified)
                {
                    verificationToken.IsVerified = true;
                    Bl.UserVertificationTokens.Update(verificationToken);
                }
            });
           return Ok("החשבון אומת בהצלחה");

        }
    }
}
