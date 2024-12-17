using delphibackend.IAM.Domain.Repositories;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace delphibackend.IAM.Infraestructure.Persistence.EFC.Repositories;

public class AuthUserRepository(AppDbContext context) : BaseRepository<Domain.Model.Aggregates.AuthUser>(context), IAuthUserRepository
{
    /**
     * <summary>
     *     Find a user by username
     * </summary>
     * <param name="email">The username to search</param>
     * <returns>The user</returns>
     */
    public async Task<Domain.Model.Aggregates.AuthUser?> FindByEmailAsync(string email)
    {
        return await Context.Set<Domain.Model.Aggregates.AuthUser>().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

    /**
     * <summary>
     *     Check if a user exists by email
     * </summary>
     * <param name="email">The username to search</param>
     * <returns>True if the user exists, false otherwise</returns>
     */
    public bool ExistsByEmail(string email)
    {
        return Context.Set<Domain.Model.Aggregates.AuthUser>().Any(user => user.Email.Equals(email));
    }
}