using Azure.Core;
using CAP_Backend_Source.Modules.Account.Request;
using CAP_Backend_Source.Modules.Programs.Request;
using CAP_Backend_Source.Modules.Programs.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    {
        int id = int.Parse(User.FindFirstValue("id").ToString());
        return Ok(await _programService.CreateAsync(id,request));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromForm] CreateProgramRequest request)
    {
        return Ok(await _programService.UpdateAsync(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _programService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return Ok(await _programService.GetDetailAsync(id));
    }


    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _programService.GetAsync());
}