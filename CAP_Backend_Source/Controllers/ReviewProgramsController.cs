using CAP_Backend_Source.Modules.ReviewProgram.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CAP_Backend_Source.Modules.ReviewProgram.Request.ReviewProgramRequest;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewProgramsController : ControllerBase
    {
        private readonly IReviewProgramService _reviewProgramService;
        public ReviewProgramsController(IReviewProgramService reviewProgramService)
        {
            _reviewProgramService = reviewProgramService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListPrograms()
        {
            return Ok(await _reviewProgramService.GetListPrograms());
        }

        [HttpGet("listprogram/{idAccount}")]
        public async Task<IActionResult> GetProgramsByIdReviewer(int idAccount)
        {
            return Ok(await _reviewProgramService.GetProgramsByIdReviewer(idAccount));
        }

        [HttpGet("listapproved/{idProgram}")]
        public async Task<IActionResult> GetApprovedListByIdProgram(int idProgram)
        {
            return Ok(await _reviewProgramService.GetApprovedListByIdProgram(idProgram));
        }

        [HttpGet("numberquestion/{idTest}")]
        public async Task<IActionResult> GetNumberQuestion(int idTest)
        {
            return Ok(await _reviewProgramService.GetNumberQuestion(idTest));
        }

        [HttpGet("numbercontent/{programId}")]
        public async Task<IActionResult> GetNumberContent(int programId)
        {
            return Ok(await _reviewProgramService.GetNumberContent(programId));
        }

        [HttpGet("status/{programId}")]
        public async Task<IActionResult> GetStatusProgram(int programId)
        {
            return Ok(await _reviewProgramService.GetStatusProgram(programId));
        }

        [HttpPost("reviewer")]
        public async Task<IActionResult> SetReviewer(CreateReviewerRequest request)
        {
            return Ok(await _reviewProgramService.SetReviewer(request));
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ApproveProgram(ApproveProgramRequest request)
        {
            return Ok(await _reviewProgramService.ApproveProgram(request));
        }

        [HttpPost("sendreviewer/{idprogram}")]
        public async Task<IActionResult> SendReviewer(int idprogram)
        {
            return Ok(await _reviewProgramService.SendReviewer(idprogram));
        }
    }
}
