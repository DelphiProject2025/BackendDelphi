using delphibackend.User.Application.Internal.QueryServices;
using delphibackend.User.Application.Internal.CommandServices;
using delphibackend.User.Domain.Services;
using delphibackend.User.Interfaces.Resources;
using delphibackend.User.Interfaces.Transform;
using delphibackend.User.Interfaces.Transform.Participant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace delphibackend.User.Interfaces.Controllers
{
    [ApiController]
    [Authorize]

    [Route("api/v1/[controller]")]
    public class HostController : ControllerBase
    {
        private readonly IHostCommandService _hostCommandService;
        private readonly IHostQueryService _hostQueryService;

        public HostController(IHostCommandService hostCommandService, IHostQueryService hostQueryService)
        {
            _hostCommandService = hostCommandService;
            _hostQueryService = hostQueryService;
        }

       

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetHostById(Guid id)
        {
            var host = await _hostQueryService.GetHostByIdAsync(id);
            if (host == null) return NotFound(new { message = "Host not found." });
            return Ok(host);
        }

        [HttpGet("exists/{authUserId:guid}")]
        public async Task<IActionResult> ExistsHostByAuthUserId(Guid authUserId)
        {
            var exists = await _hostQueryService.ExistsByAuthUserIdAsync(authUserId);
            return Ok(new { exists });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateHost(Guid id, [FromBody] UpdateHostResource resource)
        {
            var host = await _hostQueryService.GetHostByIdAsync(id);
            if (host == null) return NotFound(new { message = "Host not found." });

            UpdateHostCommandFromResourceAssembler.ApplyUpdate(host, resource);
            var success = await _hostCommandService.UpdateHostAsync(host);
            if (!success) return BadRequest(new { message = "Failed to update Host." });

            return Ok(new { message = "Host updated successfully." });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHost(Guid id)
        {
            var success = await _hostCommandService.DeleteHostAsync(id);
            if (!success) return NotFound(new { message = "Host not found." });

            return Ok(new { message = "Host deleted successfully." });
        }
        [HttpPut("{id}/display-name")]
        public async Task<IActionResult> UpdateHostDisplayName(Guid id, [FromBody] UpdateDisplayNameResource request)
        {
            var host = await _hostQueryService.GetHostByIdAsync(id);
            if (host == null) return NotFound("Host not found");

            UpdateDisplayNameFromResourceAssembler.ApplyToHost(host, request);
            var success = await _hostCommandService.UpdateHostAsync(host);
            if (!success) return BadRequest("Failed to update Host.");

            return Ok(host);
        }

    }
}
