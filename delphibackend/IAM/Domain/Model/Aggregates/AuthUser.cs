using delphibackend.User.Domain.Model.Entities;
using Newtonsoft.Json;
using Aggregates_Host = delphibackend.User.Domain.Model.Entities.Host;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.IAM.Domain.Model.Aggregates;

public class AuthUser(string email, string passwordHash,string name,string phone,DateTime registeredAt)

{

    public AuthUser(): this(string.Empty, string.Empty,string.Empty,string.Empty,DateTime.UtcNow){}
    
    public Guid Id { get; }
    
    public string Email { get; private set; } = email;
    
    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;

    public string Name { get; set; } = name; // Nombre completo

    public string PhoneNumber { get; set; } = phone;

    public DateTime RegisteredAt { get; set; } = registeredAt; // Fecha de registro

    public AuthUser updateEmail(string email)
    {
        Email = email;
        return this;
    }

    public AuthUser updatePassword(string password)
    {
        PasswordHash = password;
        return this;
    }
    
    public Aggregates_Host? Host { get; internal set; }
    public Participant? Participant { get; internal set; }

    
}