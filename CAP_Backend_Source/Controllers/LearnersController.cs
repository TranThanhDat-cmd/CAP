using CAP_Backend_Source.Modules.Learners.Requests;
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

        [Authorize]
        [HttpPost("RegisterOrUnRegister")]
        public async Task<IActionResult> RegisterOrUnRegister(RegisterOrUnRegisterRequest request)
        {
            int userID = int.Parse(User.FindFirstValue("id").ToString());

            await _learnerServices.RegisterOrUnRegisterAsync(userID, request);
            return Ok();
        }
    }
}
