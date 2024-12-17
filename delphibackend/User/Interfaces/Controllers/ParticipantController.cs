using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Domain.Services;
using delphibackend.User.Interfaces.Resources;
using Microsoft.AspNetCore.Mvc;

namespace delphibackend.User.Interfaces.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantQueryService _participantQueryService;
        private readonly IParticipantCommandService _participantCommandService;

        public ParticipantController(IParticipantQueryService participantQueryService, IParticipantCommandService participantCommandService)
        {
            _participantQueryService = participantQueryService;
            _participantCommandService = participantCommandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParticipants()
        {
            var participants = await _participantQueryService.GetAllParticipantsAsync();
            return Ok(participants);
        }

        [HttpGet("{participantId}")]
        public async Task<IActionResult> GetParticipantById(Guid participantId)
        {
            var participant = await _participantQueryService.GetParticipantByIdAsync(participantId);
            if (participant == null) return NotFound(new { message = "Participant not found." });

            return Ok(participant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipant([FromBody] CreateParticipantResource resource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var participant = await _participantCommandService.CreateParticipantAsync(
                resource.AuthUserId, resource.Role, resource.IsAnonymous);

            if (participant == null) 
                return BadRequest(new { message = "Failed to create participant." });

            return CreatedAtAction(nameof(GetParticipantById), new { participantId = participant.Id }, participant);
        }

        [HttpPut("{participantId}/role")]
        public async Task<IActionResult> UpdateParticipantRole(Guid participantId, [FromBody] UpdateParticipantResource resource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _participantCommandService.UpdateParticipantRoleAsync(participantId, resource.NewRole);
            if (!success) return BadRequest(new { message = "Failed to update participant role." });

            return Ok(new { message = "Participant role updated successfully." });
        }

        [HttpDelete("{participantId}")]
        public async Task<IActionResult> DeleteParticipant(Guid participantId)
        {
            var success = await _participantCommandService.DeleteParticipantAsync(participantId);
            if (!success) return BadRequest(new { message = "Failed to delete participant." });

            return Ok(new { message = "Participant deleted successfully." });
        }

        [HttpPut("{participantId}/deactivate")]
        public async Task<IActionResult> DeactivateParticipant(Guid participantId)
        {
            var success = await _participantCommandService.DeactivateParticipantAsync(participantId);
            if (!success) return BadRequest(new { message = "Failed to deactivate participant." });

            return Ok(new { message = "Participant deactivated successfully." });
        }
        [HttpPut("{participantId}/activate")]
        public async Task<IActionResult> ActivateParticipant(Guid participantId)
        {
            var success = await _participantCommandService.ActivateParticipantAsync(participantId);
            if (!success)
                return BadRequest(new { message = "Failed to activate participant." });

            return Ok(new { message = "Participant activated successfully." });
        }
        
        // Nuevo endpoint para activar el estado anónimo
        [HttpPut("{participantId}/activate-anonymous")]
        public async Task<IActionResult> ActivateAnonymous(Guid participantId)
        {
            var success = await _participantCommandService.ActivateAnonymousAsync(participantId);
            if (!success)
                return BadRequest(new { message = "Failed to activate anonymous state." });

            return Ok(new { message = "Participant set to anonymous successfully." });
        }

        // Nuevo endpoint para desactivar el estado anónimo
        [HttpPut("{participantId}/deactivate-anonymous")]
        public async Task<IActionResult> DeactivateAnonymous(Guid participantId)
        {
            var success = await _participantCommandService.DeactivateAnonymousAsync(participantId);
            if (!success)
                return BadRequest(new { message = "Failed to deactivate anonymous state." });

            return Ok(new { message = "Participant anonymous state removed successfully." });
        }

        [HttpGet("{authUserId}/active")]
        public async Task<IActionResult> IsUserAnActiveParticipant(Guid authUserId)
        {
            var isActive = await _participantQueryService.IsUserAnActiveParticipantAsync(authUserId);
            return Ok(new { isActive });
        }
    }
}
