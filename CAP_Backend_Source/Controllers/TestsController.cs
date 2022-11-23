using CAP_Backend_Source.Modules.Tests.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CAP_Backend_Source.Modules.Tests.Request.TestRequest;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestsController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTestByProgramId(int id)
        {
            return Ok(await _testService.GetTestByContentId(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest(CreateTestRequest request)
        {
            return Ok(await _testService.CreateTest(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTest(int id, UpdateTestRequest request)
        {
            return Ok(await _testService.UpdateTest(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            return Ok(await _testService.DeleteTest(id));
        }
    }
}
