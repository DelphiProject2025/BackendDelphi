﻿using Microsoft.AspNetCore.Mvc;
using delphibackend.Delphi.Domain.Services;
using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.Delphi.Interfaces.Resources;
using delphibackend.Delphi.Interfaces.Transform;
using delphibackend.IAM.Infraestructure.Pipeline.Middleware.Attributes;
using delphibackend.IAM.Interfaces.REST.Controllers;

namespace delphibackend.Delphi.Interfaces.Controllers;

[ApiController]
[Authorize]

[Route("api/v1/[controller]")]
public class RoomController(IRoomCommandService roomCommandService, IRoomQueryService roomQueryService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomResource createRoomResource)
    {
        var authUserId = GetAuthenticatedUserId(); // 🔥 Obtiene el ID desde el JWT

        var createRoomCommand = CreateRoomCommandFromResourceAssembler.ToCommandFromResource(createRoomResource, authUserId);
        var room = await roomCommandService.Handle(createRoomCommand);
        if (room is null) return BadRequest();
    
        return CreatedAtAction(nameof(GetRoomById), new { roomId = room.Id }, room);
    }

    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> GetRoomById([FromRoute] Guid roomId)
    {
        var room = await roomQueryService.Handle(new GetRoomByIdQuery(roomId));
        if (room is null) return BadRequest();
        var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return Ok(resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var rooms = await roomQueryService.Handle(new GetAllRoomsQuery());
        var resources = rooms.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost("joinRoom")]
    public async Task<IActionResult> AddUserToRoom([FromBody] AddParticipantToRoomResource addParticipantToRoomResource)
    {
        var authUserId = GetAuthenticatedUserId(); // 🔥 Obtiene el ID desde el JWT

        var addParticipantCommand = AddParticipantToRoomCommandFromResourceAssembler
            .ToCommandFromResource(addParticipantToRoomResource, authUserId);
        var room = await roomCommandService.Handle(addParticipantCommand);
        if (room is null) return BadRequest();
        var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return Ok(resource);
    }

    [HttpDelete("end/{roomId:guid}")]
    public async Task<IActionResult> EndRoomSession([FromRoute] Guid roomId)
    {
        var endRoomCommand = new EndRoomCommand(roomId);
        var result = await roomCommandService.Handle(endRoomCommand);
        if (!result) return BadRequest();
        return NoContent();
    }

    [HttpPut("start/{roomId:guid}")]
    public async Task<IActionResult> StartRoomSession([FromRoute] Guid roomId)
    {
        var startRoomCommand = new StartRoomCommand(roomId);
        var room = await roomCommandService.Handle(startRoomCommand);
        if (room is null) return BadRequest();
        var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return Ok(resource);
    }

    [HttpGet("participants/{roomId:guid}")]
    public async Task<IActionResult> GetParticipantsByRoomId([FromRoute] Guid roomId)
    {
        var participants = await roomQueryService.Handle(new GetParticipantsByRoomIdQuery(roomId));
        if (!participants.Any()) return NotFound();
        return Ok(participants);
    }
}
