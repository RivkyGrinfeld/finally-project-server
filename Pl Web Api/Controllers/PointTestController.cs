//using Bl.Api;
//using Bl.Models;
//using Dal.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Net.Http;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//namespace Pl_Web_Api.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class PointTestController : ControllerBase
//    {
//        IBl _bl;

//        public PointTestController(IBl blAnswersService)
//        {
//            _bl = blAnswersService;
//        }


//        [HttpPost]
//        public async Task<bool> AddTest([FromBody] CompareTo[] value, [FromQuery] string id)
//        {




//            var client = new HttpClient();

//            client.BaseAddress = new Uri("https://safeai613.com/v1/");
//            client.DefaultRequestHeaders.Authorization =
//                new AuthenticationHeaderValue("Bearer", "sk-safeai-17d8a0af5e5a359d982311ad9c4e622806cc2765c40ebbb2");






//            List<BlAnswers> Answers = await _bl.Answers.GetAll();
//            List<BlQuestions> Questions = await _bl.Questions.GetAll();
//            int AllScore = 0;
//            BlTest newTest;
//            newTest = new()
//            {
//                CustId = id,
//                Grade = ""
//            };
//            await _bl.Test.Create(newTest);
//            for (int i = 0; i < value.Length; i++)
//            {
//                int Score = 0;

//                BlPointsTest newPointTest;

//                if (Questions.Find(x => x.Id == value[i].id && x.IsAmerican) != null)
//                {
//                    BlAnswers a = Answers.Find(x => x.QuestionId.Equals(value[i].id) && x.Id.ToString() == value[i].text && x.IsCorrect);
//                    if (a != null)
//                    {
//                        Score = Questions.Find(x => x.Id == value[i].id).Score;
//                    }



//                }
//                else
//                {
//                    BlQuestions q = Questions.Find(x => x.Id == value[i].id);
//                    var payload = new
//                    {
//                        model = "gpt-4o-mini",
//                        messages = new[]
//                                   {
//                                 new { role = "system", content = " אתה בודק שאלות בכל מיני תחומים אתה מקבל שאלה ותשובה וניקוד של שאלה עליך לתת ניקוד בהתאם לתשובה אתה צריך להחזיר רק מספר ולא יותר מזה! " },
//                                 new{role = "system",content = "זה השאלה:" + q.Text},
//                                  new { role = "user", content = "זו התשובה:"+value[i].text }
//    }
//                    };

//                    var json = JsonSerializer.Serialize(payload);

//                    var response = await client.PostAsync(
//                        "chat/completions",
//                        new StringContent(json, Encoding.UTF8, "application/json")
//                    );

//                    var result = await response.Content.ReadAsStringAsync();

//                    Console.WriteLine(result);
//                    var obj = JsonSerializer.Deserialize<JsonElement>(result);
//                    string scoreText =
//     obj.GetProperty("choices")[0]
//        .GetProperty("message")
//        .GetProperty("content")
//        .GetString();
//                    Score = int.Parse(scoreText.Trim());
//                    Score = int.Parse(scoreText.Trim());
//                    Score = int.Parse(scoreText.Trim());

//                }
//                List<BlPointsTest> PointTest = _bl.PointsTest.GetAll().Result;
//                BlPointsTest q = PointTest.Find(x => x.PropertyId == Questions.Find(x => x.Id == value[i].id).PropertyId);
//                BlTest tes = _bl.Test.GetAll().Result.Find(x => x.CustId == newTest.CustId);
//                if (q != null)
//                {
//                    newPointTest = new()
//                    {
//                        Id = q.Id,
//                        TestId = tes.TestId,
//                        PropertyId = q.PropertyId,
//                        GradeProperty = Score + q.GradeProperty

//                    };
//                    await _bl.PointsTest.Update(newPointTest);
//                }
//                else
//                {
//                    newPointTest = new()
//                    {
//                        TestId = tes.TestId,
//                        PropertyId = Questions.Find(x => x.Id == value[i].id).PropertyId,
//                        GradeProperty = Score

//                    };
//                    await _bl.PointsTest.Create(newPointTest);
//                }

//            }
//            return true;

//        }
//    }

//}

using Bl.Api;
using Bl.Models;
using Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PointTestController : ControllerBase
    {
        IBl _bl;

        public PointTestController(IBl blAnswersService)
        {
            _bl = blAnswersService;
        }


        [HttpPost]
        public async Task<bool> AddTest([FromBody] CompareTo[] value, [FromQuery] string id)
        {

            var client = new HttpClient();

            client.BaseAddress = new Uri("https://safeai613.com/v1/");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "sk-safeai-17d8a0af5e5a359d982311ad9c4e622806cc2765c40ebbb2");

            List<BlAnswers> Answers = await _bl.Answers.GetAll();
            List<BlQuestions> Questions = await _bl.Questions.GetAll();
            int AllScore = 0;
            BlTest newTest;
            newTest = new()
            {
                CustId = id,
                Grade = ""
            };
            await _bl.Test.Create(newTest);
            for (int i = 0; i < value.Length; i++)
            {
                int Score = 0;

                BlPointsTest newPointTest;

                if (Questions.Find(x => x.Id == value[i].id && x.IsAmerican) != null)
                {
                    BlAnswers a = Answers.Find(x => x.QuestionId.Equals(value[i].id) && x.Id.ToString() == value[i].text && x.IsCorrect);
                    if (a != null)
                    {
                        Score = Questions.Find(x => x.Id == value[i].id).Score;
                    }



                }
                else
                {
                    BlQuestions q1 = Questions.Find(x => x.Id == value[i].id);
                    var payload = new
                    {
                        model = "gpt-4o-mini",
                        messages = new[]
                                   {
                                 new { role = "system", content = " אתה בודק שאלות בכל מיני תחומים אתה מקבל שאלה ותשובה וניקוד של שאלה עליך לתת ניקוד בהתאם לתשובה אתה צריך להחזיר רק מספר ולא יותר מזה! " },
                                 new{role = "system",content = "זה השאלה:" + q1.Text},
                                  new { role = "user", content = "זו התשובה:"+value[i].text }
    }
                    };

                    var json = JsonSerializer.Serialize(payload);

                    var response = await client.PostAsync(
                        "chat/completions",
                        new StringContent(json, Encoding.UTF8, "application/json")
                    );

                    var result = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(result);
                    var obj = JsonSerializer.Deserialize<JsonElement>(result);
                    string scoreText =
                       obj.GetProperty("choices")[0]
                          .GetProperty("message")
                          .GetProperty("content")
                          .GetString();
                    Score = int.Parse(scoreText.Trim());

                }
                List<BlPointsTest> PointTest = _bl.PointsTest.GetAll().Result;
                BlTest tes = _bl.Test.GetAll().Result.Find(x => x.CustId == newTest.CustId);
                BlPointsTest q = PointTest.Find(x => x.PropertyId == Questions.Find(x => x.Id == value[i].id).PropertyId && x.TestId == tes.TestId);
                
                if (q != null)
                {
                    newPointTest = new()
                    {
                        Id = q.Id,
                        TestId = tes.TestId,
                        PropertyId = q.PropertyId,
                        GradeProperty = Score + q.GradeProperty

                    };
                    await _bl.PointsTest.Update(newPointTest);
                }
                else
                {
                    newPointTest = new()
                    {
                        TestId = tes.TestId,
                        PropertyId = Questions.Find(x => x.Id == value[i].id).PropertyId,
                        GradeProperty = Score

                    };
                    await _bl.PointsTest.Create(newPointTest);
                }

            }
            return true;

        }

        [HttpGet]
        public async Task<IActionResult> GetByCustId([FromQuery] string id)
        {
            var tests = await _bl.Test.GetAll();
            var test = tests.Find(x => x.CustId == id);
            if (test == null)
                return NotFound(new { error = "לא נמצא מבחן עבור מועמד זה" });

            var allPoints = await _bl.PointsTest.GetAll();
            var points = allPoints.Where(p => p.TestId == test.TestId).ToList();

            return Ok(new
            {
                testId = test.TestId,
                custId = test.CustId,
                grade = test.Grade,
                pointsTest = points.Select(p => new
                {
                    id = p.Id,
                    testId = p.TestId,
                    propertyId = p.PropertyId,
                    gradeProperty = p.GradeProperty
                })
            });
        }
    }

}

