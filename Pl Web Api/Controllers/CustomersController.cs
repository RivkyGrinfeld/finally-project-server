using Bl.Api;
using Bl.Models;
using Bl.Services;
using Dal.Api;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        IBl bl;
        //private readonly VerificationCodeBL _codeBL;
        public CustomersController(IBl bl)
        {
            this.bl = bl;
        }
        [HttpGet]
        public async Task<List<BlCustomer>> GetAll()
        {
            return await bl.Customers.GetAll();
        }

        [HttpGet]
        public async Task<BlCustomer> GetByUserId(int userId)
        {
            return await bl.Customers.GetByUserId(userId);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromForm] BlCustomer customer, IFormFile cvFile)
        {
            if (cvFile != null && cvFile.Length > 0)
            {
                // שמירת הקובץ
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}_{cvFile.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await cvFile.CopyToAsync(stream);
                }

                // שמירת שם הקובץ והנתיב במודל
                customer.FileName = uniqueFileName;
                customer.Url = filePath;
            }
            BlUser newus = await bl.Users.Create(new BlUser() { Password = customer.Password, UserName = customer.UserName, StatusId = 3 });
            //newus = bl.Users.GetAll().Result.Find(x => x.UserName == newus.UserName && x.Password == newus.Password && x.StatusId == newus.StatusId);
            // יצירת הלקוח דרך BL
            customer.UserId = newus.Id;
            var result = await bl.Customers.Create(customer);



                var webhookUrl = "https://n8n.srv1251456.hstgr.cloud/webhook-test/5f00722d-a80a-42f5-a8c2-ac3a382fc362";
                var filePath1 = @"file:///D:/hs.pdf";

                using var client = new HttpClient();
                using var form = new MultipartFormDataContent();
                using var fileStream = System.IO.File.OpenRead(filePath1);
                {
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                    form.Add(fileContent, "file", System.IO.Path.GetFileName(filePath1));

                    var response = await client.PostAsync(webhookUrl, form);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("File sent successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
        



            if (result)
                return Ok(new { success = true, message = "Customer created and verification code sent." });

            // אחרי יצירת הלקוח, יש ליצור את קוד האימות ולשלוח אותו
            //if (result)
            //{
            //    //var verificationCode = await bl.VerificationCodes.GenerateAndSendCodeAsync(customer.Email);
            //    return Ok(new { success = true, message = "Customer created and verification code sent." });
            //}
            else
                return StatusCode(500, "Error creating customer");
        }
    }
}


//using Bl.Api;
//using Bl.Models;
//using Bl.Services;
//using Dal.Api;
//using Microsoft.AspNetCore.Mvc;

//namespace Pl_Web_Api.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class CustomersController : ControllerBase
//    {
//        private readonly IBl bl;
//        private readonly VerificationCodeBL _codeBL;

//        // הזרקת IBl ו-VerificationCodeBL
//        public CustomersController(IBl bl, VerificationCodeBL codeBL)
//        {
//            this.bl = bl;
//            _codeBL = codeBL;  // הזרקת שירות של קוד אימות
//        }

//        // פונקציה לשליפת כל הלקוחות
//        [HttpGet]
//        public async Task<List<BlCustomer>> GetAll()
//        {
//            return await bl.Customers.GetAll();
//        }

//        // פונקציה להוספת לקוח חדש
//        [HttpPost]
//        public async Task<IActionResult> AddCustomer([FromForm] BlCustomer customer, IFormFile cvFile)
//        {
//            if (cvFile != null && cvFile.Length > 0)
//            {
//                // שמירת הקובץ
//                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
//                if (!Directory.Exists(uploadsFolder))
//                    Directory.CreateDirectory(uploadsFolder);

//                var uniqueFileName = $"{Guid.NewGuid()}_{cvFile.FileName}";
//                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

//                using (var stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await cvFile.CopyToAsync(stream);
//                }

//                // שמירת שם הקובץ והנתיב במודל
//                customer.FileName = uniqueFileName;
//                customer.Url = filePath;
//            }

//            // יצירת הלקוח דרך BL
//            var result = await bl.Customers.Create(customer);

//            // אחרי יצירת הלקוח, יש ליצור את קוד האימות ולשלוח אותו
//            if (result)
//            {
//                // יצירת קוד אימות ושליחתו ללקוח
//                var verificationCode = await _codeBL.GenerateAndSendCodeAsync(customer.Email);
//                return Ok(new { success = true, message = "Customer created and verification code sent." });
//            }
//            else
//            {
//                return StatusCode(500, "Error creating customer");
//            }
//        }
//    }
//}
