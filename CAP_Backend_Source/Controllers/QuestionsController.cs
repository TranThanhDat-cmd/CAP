using CAP_Backend_Source.Modules.Question.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CAP_Backend_Source.Modules.Question.Request.QuestionRequest;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListQuestionByTestId(int id)
        {
            return Ok(await _questionService.GetListQuestionByTestId(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateQuestionRequest request)
        {
            int id = await _questionService.CreateQuestion(request);

            return Ok(await _questionService.CreateQuestionContent(id, request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, UpdateQuestionRequest request)
        {
            return Ok(await _questionService.UpdateQuestion(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            return Ok(await _questionService.DeleteQuestion(id));
        }

        [HttpDelete("content/{id}")]
        public async Task<IActionResult> DeleteQuestionContent(int id)
        {
            return Ok(await _questionService.DeleteQuestionContent(id));
        }
    }
}
