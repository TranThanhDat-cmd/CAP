using CAP_Backend_Source.Modules.TypeTest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly ITypeTestService _typeTestService;
        public TypesController(ITypeTestService typeTestService)
        {
            _typeTestService = typeTestService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _typeTestService.GetAll());
        }
    }
}
