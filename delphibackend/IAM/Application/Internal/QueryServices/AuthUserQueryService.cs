using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.IAM.Domain.Model.Queries;
using delphibackend.IAM.Domain.Repositories;
using delphibackend.IAM.Domain.Services;

namespace delphibackend.IAM.Application.Internal.QueryServices;


public class AuthUserQueryService(IAuthUserRepository userRepository) : IAuthUserQueryService
{
    /**
     * <summary>
     *     Handle get user by id query
     * </summary>
     * <param name="query">The query object containing the user id to search</param>
     * <returns>The user</returns>
     */
    public async Task<Domain.Model.Aggregates.AuthUser?> Handle(GetAuthUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }

    /**
     * <summary>
     *     Handle get user by username query
     * </summary>
     * <param name="query">The query object for getting all users</param>
     * <returns>The user</returns>
     */
    public async Task<IEnumerable<Domain.Model.Aggregates.AuthUser>> Handle(GetAllAuthUsersQuery query)
    {
        return await userRepository.ListAsync();
    }

    /**
     * <summary>
     *     Handle get user by username query
     * </summary>
     * <param name="query">The query object containing the username to search</param>
     * <returns>The user</returns>
     */
    public async Task<Domain.Model.Aggregates.AuthUser?> Handle(GetAuthUserByEmailQuery query)
    {
        return await userRepository.FindByEmailAsync(query.Email);
    }
    public async Task<UserRoleResponse?> Handle(GetUserRoleQuery query)
    {
        var user = await userRepository.FindByIdAsync(query.AuthUserId);

        if (user == null)
            return null;

        if (user.Host != null)
        {
            return new UserRoleResponse
            {
                Role = "Host",
                Id = user.Host.Id
            };
        }

        if (user.Participant != null)
        {
            return new UserRoleResponse
            {
                Role = "Participant",
                Id = user.Participant.Id
            };
        }

        return new UserRoleResponse
        {
            Role = "None",
            Id = null
        };
    }



}