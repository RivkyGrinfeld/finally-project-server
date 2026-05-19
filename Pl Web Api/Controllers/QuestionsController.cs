using Bl.Api;
using Bl.Models;
using Bl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class QuestionsController : ControllerBase
    {
        IBl _bl;

        public QuestionsController(IBl blQuestionsService)
        {
            _bl = blQuestionsService;
        }

        // Endpoint לשליפת שאלה לפי ID
        [HttpGet("{id}")]
        public async Task<BlQuestions> Get(int id)
        {
            BlQuestions question = await _bl.Questions.GetById(id);
            if (question == null)
                return null;
            return question;
        }

        // Endpoint לשליפת כל השאלות
        [HttpGet]
        public async Task<List<BlQuestions>> GetAll()
        {
            List<BlQuestions> questions = await _bl.Questions.GetAll();
            return questions;
        }

        // Endpoint ליצירת שאלה
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlQuestions blQuestion)
        {
            var success = await _bl.Questions.Create(blQuestion);
            if (success)
                return CreatedAtAction(nameof(Get), new { id = blQuestion.Id }, blQuestion);
            return BadRequest("Error creating question");
        }

        // Endpoint לעדכון שאלה
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BlQuestions blQuestion)
        {
            var success = await _bl.Questions.Update(blQuestion);
            if (success)
                return NoContent();
            return BadRequest("Error updating question");
        }

        // Endpoint למחיקת שאלה
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var question = await _bl.Questions.GetById(id);
            if (question == null)
                return NotFound();

            var success = await _bl.Questions.Delete(question);
            if (success)
                return NoContent();
            return BadRequest("Error deleting question");
        }
    }
}

