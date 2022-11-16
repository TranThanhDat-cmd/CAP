using CAP_Backend_Source.Modules.Programs.Request;
using CAP_Backend_Source.Modules.Programs.Service;
using Microsoft.AspNetCore.Mvc;

namespace CAP_Backend_Source.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgramsController : ControllerBase
{
    private readonly IProgramService _programService;

    public ProgramsController(IProgramService programService)
    {
        _programService = programService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm]CreateProgramRequest request)
        => Ok(await _programService.CreateAsync(request));
    
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _programService.GetAsync());
}