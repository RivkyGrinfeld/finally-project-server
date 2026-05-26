using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pl_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandwritingController : ControllerBase
    {
        private readonly OpenAiHandwritingService _service;

        public HandwritingController(OpenAiHandwritingService service)
        {
            _service = service;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            using var stream = file.OpenReadStream();

            var result = await _service.AnalyzeHandwriting(stream);

            return Ok(new
            {
                text = result
            });
        }
    }
}
