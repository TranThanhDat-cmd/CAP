using CAP_Backend_Source.Modules.MultipleChoiceQuestion.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CAP_Backend_Source.Modules.MultipleChoiceQuestion.Request.MCQuestionRequest;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MCQuestionsController : ControllerBase
    {
        private readonly IMCQuestionService _mCQuestionService;
        public MCQuestionsController(IMCQuestionService mCQuestionService)
        {
            _mCQuestionService = mCQuestionService;
        }

        [HttpGet("getlist/{testId}")]
        public async Task<IActionResult> GetQuestionByTestId(int testId)
        {
            return Ok(await _mCQuestionService.GetQuestionByTestId(testId));
        }

        [HttpGet("getone/{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            return Ok(await _mCQuestionService.GetQuestionById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateMCQuestionRequest request)
        {
            return Ok(await _mCQuestionService.CreateQuestion(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, UpdateMCQuestionRequest request)
        {
            return Ok(await _mCQuestionService.UpdateQuestion(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            return Ok(await _mCQuestionService.DeleteQuestion(id));
        }
    }
}
