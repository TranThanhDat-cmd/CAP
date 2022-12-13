﻿using CAP_Backend_Source.Modules.ReviewProgram.Service;
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

        [HttpGet("listprogram/{idReviewer}")]
        public async Task<IActionResult> GetProgramsByIdReviewer(int idReviewer)
        {
            return Ok(await _reviewProgramService.GetProgramsByIdReviewer(idReviewer));
        }

        [HttpGet("listapproved/{idProgram}")]
        public async Task<IActionResult> GetApprovedListByIdProgram(int idProgram)
        {
            return Ok(await _reviewProgramService.GetApprovedListByIdProgram(idProgram));
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
    }
}