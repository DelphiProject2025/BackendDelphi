using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using delphibackend.Delphi.Application.Internal.CommandServices;
using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Services;
using delphibackend.Delphi.Interfaces.Hubs;
using delphibackend.Delphi.Interfaces.Resources;
using Microsoft.AspNetCore.Authorization;

namespace delphibackend.Delphi.Interfaces.Controllers;

[ApiController]
[Authorize]

[Route("api/v1/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IRoomCommandService _roomCommandService;

    public ChatController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }


    [HttpPost("join")]
    public IActionResult JoinRoom([FromBody] JoinRoomRequest request)
    {
        return Ok(new { message = "Successfully joined the room." });
    }

    [HttpPost("check-activated")]
    public async Task<IActionResult> CheckIfActivated([FromBody] CheckIfActivatedRequest request)
    {
        var checkIfActivatedCommand = new CheckIfActivatedCommand(request.RoomId, request.HostId);
        var result = await _roomCommandService.Handle(checkIfActivatedCommand);
        if (result)
        {
            return Ok(new { message = "Room and Chat are active." });
        }
        else
        {
            return BadRequest(new { message = "Cannot join the room." });
        }
    }
    }
   
    
 