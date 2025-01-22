using delphibackend.IAM.Domain.Model.Aggregates;

namespace delphibackend.User.Domain.Model.Entities;

public class Host
{
    public Host()
    {
        CreatedAt = DateTime.UtcNow;
    }
    public Host(Guid id) : this()
    {
        Id = id;
    }
    public Host(AuthUser authUser) : this()
    {
        AuthUserId = authUser.Id;
        AuthUser = authUser;
        IsActive = true; // Configurar como activo por defecto
    }

    public Guid Id { get; set; } 
    public Guid AuthUserId { get; set; }
    public DateTime CreatedAt { get; set; } 
    public bool IsActive { get; set; } // Estado activo/inactivo del Host

    public AuthUser? AuthUser { get; set; }
    
}