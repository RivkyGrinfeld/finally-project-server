using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Graphologi
    {
        static async Task Main()
        {
            var client = new HttpClient();

            string url = "https://n8n.srv1251456.hstgr.cloud/webhook-test/de2581e1-919e-4877-a6f8-ae196e1584eb";

            // הנתיב לקובץ במחשב שלך
            string filePath = @"F:\תיקייה כללית חדש\שנה א תשפה\תלמידות\כוכבים.txt";

            // יצירת תוכן מסוג multipart/form-data
            using var content = new MultipartFormDataContent();

            // קריאת הקובץ
            var fileStream = File.OpenRead(filePath);
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            // הוספת הקובץ ל-body עם שם השדה "file"
            content.Add(fileContent, "file", Path.GetFileName(filePath));

            // שליחת POST
            var response = await client.PostAsync(url, content);

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
    }
}