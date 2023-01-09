using CAP_Backend_Source.Modules.DoTest.Request;
using CAP_Backend_Source.Modules.DoTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoTestsController : ControllerBase
    {
        private readonly IDoTestService _doTestService;

        public DoTestsController(IDoTestService doTestService) { _doTestService = doTestService; }

        [HttpPost("{idAccount}")]
        public async Task<IActionResult> DoTest(int idAccount, List<DoTestRequest> requests)
        {
            return Ok(await _doTestService.SaveAnswer(idAccount, requests));
        }
    }
}
