using System.Text.Json.Serialization;
using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.IAM.Domain.Model.Aggregates;

namespace delphibackend.User.Domain.Model.Entities;

public class Host
{
    public Host()
    {
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public Host(Guid id) : this()
    {
        Id = id;
    }

    public Host(AuthUser authUser, Room room) : this()
    {
        AuthUserId = authUser.Id;
        AuthUser = authUser;
        RoomId = room.Id;
        IsActive = true; // Configurar como activo por defecto
    }

    public Guid Id { get; set; }
    public Guid AuthUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } // Estado activo/inactivo del Host

   [JsonIgnore] public AuthUser? AuthUser { get; set; }
   [JsonIgnore] public Room? Room { get; set; }
    public Guid? RoomId { get; set; }
    public string DisplayName { get; set; } = "Host"; 


}