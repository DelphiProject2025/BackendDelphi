using delphibackend.User.Application.Internal.QueryServices;
using delphibackend.User.Application.Internal.CommandServices;
using delphibackend.User.Domain.Services;
using delphibackend.User.Interfaces.Resources;
using delphibackend.User.Interfaces.Transform;
using Microsoft.AspNetCore.Mvc;

namespace delphibackend.User.Interfaces.Controllers
{
    [ApiController]
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

        [HttpPost]
        public async Task<IActionResult> CreateHost([FromBody] CreateHostResource resource)
        {
            var host = await _hostCommandService.CreateHostAsync(resource.AuthUserId);
            return CreatedAtAction(nameof(GetHostById), new { id = host.Id }, host);
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
    }
}
