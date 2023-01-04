﻿using CAP_Backend_Source.Modules.Learners.Requests;
using CAP_Backend_Source.Modules.Learners.Services;
using CAP_Backend_Source.Modules.Programs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnersController : ControllerBase
    {
        private ILearnerServices _learnerServices;

        public LearnersController(ILearnerServices learnerServices)
        {
            _learnerServices = learnerServices;
        }

        [HttpGet("GetListLearner/{idProgram}")]
        public async Task<IActionResult> GetListLearners(int idProgram)
        {
            return Ok(await _learnerServices.GetListLearners(idProgram));
        }

        [HttpPost("AddLearner")]
        public async Task<IActionResult> AddLearner(AddLearnerRequest request)
        {
            return Ok(await _learnerServices.AddLearner(request));
        }

        [HttpPut("UpdateLearner/{idLearner}")]
        public async Task<IActionResult> UpdateLearner(int idLearner, UpdateLearnerRequest request)
        {
            return Ok(await _learnerServices.UpdateLearner(idLearner, request));
        }

        [Authorize]
        [HttpPost("RegisterOrUnRegister")]
        public async Task<IActionResult> RegisterOrUnRegister(RegisterOrUnRegisterRequest request)
        {
            int userID = int.Parse(User.FindFirstValue("id").ToString());

            await _learnerServices.RegisterOrUnRegisterAsync(userID, request);
            return Ok();
        }

        [Authorize]
        [HttpPost("Import")]
        public async Task<IActionResult> Import(ImportLearnerRequest request)
        {

            await _learnerServices.ImportAsync(request);
            return Ok();
        }
    }
}
