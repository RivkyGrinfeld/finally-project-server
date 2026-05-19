using Bl.Api;
using Bl.Services;
using Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
     
            IBl _bl;

            public AnswersController(IBl blAnswersService)
            {
                _bl = blAnswersService;
            }

            // Endpoint לשליפת תשובה לפי ID
            [HttpGet("{id}")]
            public async Task<BlAnswers> Get(int id)
            {
                var answer = await _bl.Answers.GetById(id);
                if (answer == null)
                    return null;
                return answer;
            }

            // Endpoint לשליפת כל התשובות
            [HttpGet]
            public async Task<List<BlAnswers>> GetAll()
            {
                return await _bl.Answers.GetAll();                 
            }

            // Endpoint ליצירת תשובה
            [HttpPost]
            public async Task<IActionResult> Create([FromBody] BlAnswers blAnswer)
            {
                var success = await _bl.Answers.Create(blAnswer);
                if (success)
                    return CreatedAtAction(nameof(Get), new { id = blAnswer.Id }, blAnswer);
                return BadRequest("Error creating answer");
            }

            // Endpoint לעדכון תשובה
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] BlAnswers blAnswer)
            {
                var success = await _bl.Answers.Update(blAnswer);
                if (success)
                    return NoContent();
                return BadRequest("Error updating answer");
            }

            // Endpoint למחיקת תשובה
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var answer = await _bl.Answers.GetById(id);
                if (answer == null)
                    return NotFound();

                var success = await _bl.Answers.Delete(answer);
                if (success)
                    return NoContent();
                return BadRequest("Error deleting answer");
            }
        }
    }

 