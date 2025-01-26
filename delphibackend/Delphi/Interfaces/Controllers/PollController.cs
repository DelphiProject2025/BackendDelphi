using Microsoft.AspNetCore.Mvc;
using delphibackend.Delphi.Application.Internal.CommandServices;
using delphibackend.Delphi.Application.Internal.QueryServices;
using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Services;

namespace delphibackend.Delphi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PollController : ControllerBase
{
    private readonly IPollCommandService _pollCommandService;
    private readonly IPollQueryService _pollQueryService;

    public PollController(IPollCommandService pollCommandService, IPollQueryService pollQueryService)
    {
        _pollCommandService = pollCommandService;
        _pollQueryService = pollQueryService;
    }

    // 1. Get polls by RoomId
    [HttpGet("room/{roomId}")]
    public async Task<IActionResult> GetPollsByRoomId(Guid roomId)
    {
        var polls = await _pollQueryService.GetPollsByRoomIdAsync(roomId);
        return Ok(polls);
    }

    // 2. Create a new poll
    [HttpPost]
    public async Task<IActionResult> CreatePoll([FromBody] CreatePollCommand command)
    {
        var pollId = await _pollCommandService.Handle(command);
        return CreatedAtAction(nameof(GetPollById), new { pollId }, null);
    }

    [HttpGet("{pollId}")]
    public async Task<IActionResult> GetPollById(Guid pollId)
    {
        var poll = await _pollQueryService.GetPollByIdAsync(pollId);
        if (poll == null)
            return NotFound();

        return Ok(poll);
    }

    // 4. Close a poll
    [HttpPost("{pollId}/close")]
    public async Task<IActionResult> ClosePoll(Guid pollId, [FromBody] ClosePollCommand command)
    {
        if (pollId != command.PollId) return BadRequest("Poll ID mismatch.");

        await _pollCommandService.Handle(command);
        return NoContent();
    }

    // 5. Vote for a poll option
    [HttpPost("{pollId}/vote")]
    public async Task<IActionResult> VotePollOption(Guid pollId, [FromBody] VotePollOptionCommand command)
    {
        if (pollId != command.PollId) return BadRequest("Poll ID mismatch.");

        await _pollCommandService.Handle(command);
        return Ok();
    }
}
