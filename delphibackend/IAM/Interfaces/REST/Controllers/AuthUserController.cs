using System.Net.Mime;
using delphibackend.IAM.Domain.Model.Queries;
using delphibackend.IAM.Domain.Services;
using delphibackend.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using delphibackend.User.Domain.Model.Queries;
using delphibackend.User.Domain.Model.Queries.Participant;
using delphibackend.User.Domain.Services;
using delphibackend.User.Interfaces.Transform;

namespace delphibackend.IAM.Interfaces.REST.Controllers;

[Authorize]
[ApiController]
[Route("delphibackend/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthUserController(IAuthUserQueryService authUserQueryService, IAuthUserCommandService userCommandService) : ControllerBase
{
    /**
     * <summary>
     *     Get user by id endpoint. It allows to get a user by id
     * </summary>
     * <param name="authUserId">The user id</param>
     * <returns>The user resource</returns>
     */
    [HttpGet("{authUserId}")]
    public async Task<IActionResult> GetAuthUserById(Guid authUserId)
    {
        var getAuthUserByIdQuery = new GetAuthUserByIdQuery(authUserId);
        var user = await authUserQueryService.Handle(getAuthUserByIdQuery);
        var userResource = AuthUserResourceFromEntityAssembler.ToResourceFromEntity(user!);
        return Ok(userResource);
    }

    /**
     * <summary>
     *     Get all users endpoint. It allows to get all users
     * </summary>
     * <returns>The user resources</returns>
     */
    [HttpGet]
    public async Task<IActionResult> GetAuthAllUsers()
    {
        var getAuthAllUsersQuery = new GetAllAuthUsersQuery();
        var users = await authUserQueryService.Handle(getAuthAllUsersQuery);
        var userResources = users.Select(AuthUserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }
    
   /* [HttpGet("{authUserId}/participants")]
    public async Task<IActionResult> GetUserByAuthUserId([FromRoute] Guid authUserId)
    {
        var user = await participantQueryService.Handle(new GetParticipantByAuthUserIdQuery(authUserId));
        if (user == null) return NotFound();
        var resource = ParticipantResourceFromEntitiyAssembler.ToResourceFromEntity(user);
        return Ok(resource);
    }
  */
    
    
}