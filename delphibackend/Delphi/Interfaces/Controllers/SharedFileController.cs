using Microsoft.AspNetCore.Mvc;
using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.Delphi.Domain.Services;
using delphibackend.Delphi.Interfaces.Transform;

namespace delphibackend.Delphi.Interfaces.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SharedFileController : ControllerBase
{
    private readonly ISharedFileQueryService _sharedFileQueryService;

    public SharedFileController(ISharedFileQueryService sharedFileQueryService)
    {
        _sharedFileQueryService = sharedFileQueryService;
    }

    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> GetSharedFileWithQuestionsByRoomId([FromRoute] Guid roomId)
    {
        var query = new GetSharedFileWithQuestionsByRoomIdQuery(roomId);
        var result = await _sharedFileQueryService.Handle(query);
        if (result == (null, null)) return NotFound(new { message = "Room or Shared File not found." });

        var resource = SharedFileWithQuestionsResourceAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpGet("getbyid/{sharedFileId:guid}")]
    public async Task<IActionResult> GetSharedFileById([FromRoute] Guid sharedFileId)
    {
        var query = new GetSharedContentByIdQuery(sharedFileId); // Reemplazo correcto
        var result = await _sharedFileQueryService.Handle(query);
        if (result == null)
            return NotFound(new { message = "Shared File not found." });

        var resource = SharedFileResourceAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
}