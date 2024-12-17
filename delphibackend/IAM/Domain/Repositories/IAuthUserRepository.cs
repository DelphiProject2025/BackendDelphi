using delphibackend.IAM.Domain.Model.Aggregates;
using delphibackend.Shared.Domain.Repositories;

namespace delphibackend.IAM.Domain.Repositories;

public interface IAuthUserRepository : IBaseRepository<AuthUser>
{
    /**
     * <summary>
     *     Find a user by id
     * </summary>
     * <param name="email">The username to search</param>
     * <returns>The user</returns>
     */
    Task<AuthUser?> FindByEmailAsync(string email);

    /**
     * <summary>
     *     Check if a user exists by username
     * </summary>
     * <param name="email">The username to search</param>
     * <returns>True if the user exists, false otherwise</returns>
     */
    bool ExistsByEmail(string email);
}