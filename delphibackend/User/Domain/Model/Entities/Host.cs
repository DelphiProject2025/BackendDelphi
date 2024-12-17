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

    public Guid Id { get; set; } 
    public Guid AuthUserId { get; set; }
    public DateTime CreatedAt { get; set; } 
    
    public AuthUser? AuthUser { get; set; }
    
}