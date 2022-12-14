using CAP_Backend_Source.Modules.Programs.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearController : ControllerBase
    {
        private IAcademicYearService _academicYearService;

        public AcademicYearController(IAcademicYearService academicYearService)
        {
            _academicYearService = academicYearService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _academicYearService.GetAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
            => Ok(await _academicYearService.DetailAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BaseActionAcademicYear request)
            => Ok(await _academicYearService.UpdateAsync(id, request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _academicYearService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] BaseActionAcademicYear request)
        {
            return Ok(await _academicYearService.CreateAsync(request));
        }
    }
}
