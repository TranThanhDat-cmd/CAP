using CAP_Backend_Source.Modules.EssayQuestion.Service;
using CAP_Backend_Source.Modules.MultipleChoiceQuestion.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CAP_Backend_Source.Modules.EssayQuestion.Request.EQuestionRequest;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EQuestionsController : ControllerBase
    {
        private readonly IEQuestionService _eQuestionService;
        public EQuestionsController(IEQuestionService eQuestionService)
        {
            _eQuestionService = eQuestionService;
        }

        [HttpGet("getlist/{testId}")]
        public async Task<IActionResult> GetQuestionByTestId(int testId)
        {
            return Ok(await _eQuestionService.GetQuestionByTestId(testId));
        }
        [HttpGet("getone/{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            return Ok(await _eQuestionService.GetQuestionById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateEQuestionRequest request)
        {
            return Ok(await _eQuestionService.CreateQuestion(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, UpdateEQuestionRequest request)
        {
            return Ok(await _eQuestionService.UpdateQuestion(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            return Ok(await _eQuestionService.DeleteQuestion(id));
        }
    }
}
