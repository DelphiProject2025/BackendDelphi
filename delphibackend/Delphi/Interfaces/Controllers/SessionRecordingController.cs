using Microsoft.AspNetCore.Mvc;
using delphibackend.Delphi.Application.Internal.QueryServices;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using delphibackend.Delphi.Domain.Services;
using delphibackend.IAM.Infraestructure.Pipeline.Middleware.Attributes;

namespace delphibackend.Delphi.Interfaces.Controllers;

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SessionRecordingController : ControllerBase
    {
        private readonly ISessionRecordingQueryService _sessionRecordingQueryService;

        public SessionRecordingController(ISessionRecordingQueryService sessionRecordingQueryService)
        {
            _sessionRecordingQueryService = sessionRecordingQueryService;
        }

        // Obtener todas las grabaciones de una sala
        [HttpGet("room/{roomId}")]
        public async Task<IActionResult> GetSessionRecordingsByRoomId(Guid roomId)
        {
            var recordings = await _sessionRecordingQueryService.Handle(new GetSessionRecordingsByRoomIdQuery(roomId));
            return Ok(recordings);
        }

        // Obtener una grabación específica por su Id
        [HttpGet("{sessionRecordingId}")]
        public async Task<IActionResult> GetSessionRecordingById(Guid sessionRecordingId)
        {
            var recording = await _sessionRecordingQueryService.Handle(new GetSessionRecordingByIdQuery(sessionRecordingId));
            if (recording == null)
                return NotFound();

            return Ok(recording);
        }
    }
