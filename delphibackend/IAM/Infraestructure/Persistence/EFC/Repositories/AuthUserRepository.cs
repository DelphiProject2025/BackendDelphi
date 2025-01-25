using delphibackend.IAM.Domain.Model.Aggregates;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace delphibackend.IAM.Domain.Repositories;

public class AuthUserRepository : BaseRepository<AuthUser>, IAuthUserRepository
{
    private readonly AppDbContext _context;

    public AuthUserRepository(AppDbContext context) : base(context)
    {
        _context = context; // Inicializa el contexto
    }

    /// <summary>
    /// Encuentra un usuario por su correo electrónico
    /// </summary>
    /// <param name="email">El correo electrónico del usuario</param>
    /// <returns>El usuario si es encontrado</returns>
    public async Task<AuthUser?> FindByEmailAsync(string email)
    {
        return await _context.Set<AuthUser>()
            .FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

    /// <summary>
    /// Encuentra un usuario por su ID
    /// </summary>
    /// <param name="id">El ID del usuario</param>
    /// <returns>El usuario si es encontrado</returns>
    public async Task<AuthUser?> FindByIdAsync(Guid authUserId)
    {
        return await _context.AuthUsers
            .Include(u => u.Participant) // Incluye la relación Participant
            .Include(u => u.Host)        // Incluye la relación Host
            .FirstOrDefaultAsync(u => u.Id == authUserId); // Filtra por Id
    }
    /// <summary>
    /// Verifica si un usuario existe por su correo electrónico
    /// </summary>
    /// <param name="email">El correo electrónico del usuario</param>
    /// <returns>True si existe, False en caso contrario</returns>
    public bool ExistsByEmail(string email)
    {
        return _context.Set<AuthUser>().Any(user => user.Email.Equals(email));
    }
}