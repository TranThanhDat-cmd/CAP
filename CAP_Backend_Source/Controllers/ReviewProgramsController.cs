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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgramsByIdReviewer(int id)
        {
            return Ok(await _reviewProgramService.GetProgramsByIdReviewer(id));
        }

        [HttpPost]
        public async Task<IActionResult> SetReviewer(CreateReviewer request)
        {
            return Ok(await _reviewProgramService.SetReviewer(request));
        }
    }
}
