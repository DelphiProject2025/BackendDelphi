using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.IAM.Domain.Model.Queries;

namespace delphibackend.IAM.Domain.Services;

public interface IAuthUserQueryService
{
    /**
     * <summary>
     *     Handle get user by id query
     * </summary>
     * <param name="query">The get user by id query</param>
     * <returns>The user if found, null otherwise</returns>
     */
    Task<Model.Aggregates.AuthUser?> Handle(GetAuthUserByIdQuery query);

    /**
     * <summary>
     *     Handle get all users query
     * </summary>
     * <param name="query">The get all users query</param>
     * <returns>The list of users</returns>
     */
    Task<IEnumerable<Model.Aggregates.AuthUser>> Handle(GetAllAuthUsersQuery query);
    
    /**
     * <summary>
     *     Handle get user by username query
     * </summary>
     * <param name="query">The get user by username query</param>
     * <returns>The user if found, null otherwise</returns>
     */
    Task<Model.Aggregates.AuthUser?> Handle(GetAuthUserByEmailQuery query);
    Task<UserRoleResponse?> Handle(GetUserRoleQuery query);

}