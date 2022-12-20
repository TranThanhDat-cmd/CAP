using Azure.Core;
using CAP_Backend_Source.Modules.Account.Request;
using CAP_Backend_Source.Modules.Programs.Request;
using CAP_Backend_Source.Modules.Programs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CAP_Backend_Source.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProgramsController : ControllerBase
{
    private readonly IProgramService _programService;

    public ProgramsController(IProgramService programService)
    {
        _programService = programService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProgramRequest request)
    {
        int id = int.Parse(User.FindFirstValue("id").ToString());
        return Ok(await _programService.CreateAsync(id, request));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromForm] CreateProgramRequest request)
    {
        return Ok(await _programService.UpdateAsync(id, request));
    }

    [HttpPut("{id}/Status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int id, [FromForm] UpdateStatusRequest request)
    {
        return Ok(await _programService.UpdateStatus(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _programService.DeleteAsync(id);
        return Ok();
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _programService.GetAsync());
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return Ok(await _programService.GetDetailAsync(id));
    }

    [AllowAnonymous]

    [HttpGet("{id}/Contents")]
    public async Task<IActionResult> GetContents([FromRoute] int id)
    {
        return Ok(await _programService.GetContentsAsync(id));
    }

    [AllowAnonymous]
    [HttpGet("/api/Contents/{contentId}")]
    public async Task<IActionResult> GetContent([FromRoute] int contentId)
    {
        return Ok(await _programService.GetContentAsync(contentId));
    }

    [HttpDelete("/api/Contents/{contentId}")]
    public async Task<IActionResult> DeleteContent([FromRoute] int contentId)
    {
        await _programService.DeleteContentAsync(contentId);
        return Ok();
    }

    [HttpPost("/api/Contents")]
    public async Task<IActionResult> CreateContent(CreateContentRequest request)
    {

        return Ok(await _programService.CreateContentAsync(request));
    }

    [HttpPut("/api/Contents/{contentId}")]
    public async Task<IActionResult> UpdateContent([FromRoute] int contentId, [FromBody] CreateContentRequest request)
    {

        return Ok(await _programService.UpdateContentAsync(contentId, request));
    }

}