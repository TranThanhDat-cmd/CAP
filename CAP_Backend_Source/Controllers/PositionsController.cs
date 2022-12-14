using CAP_Backend_Source.Modules.Programs.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _positionService.GetAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
            => Ok(await _positionService.DetailAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] string name)
            => Ok(await _positionService.UpdateAsync(id, name));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _positionService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] string name)
        {
            return Ok(await _positionService.CreateAsync(name));
        }
    }
}
