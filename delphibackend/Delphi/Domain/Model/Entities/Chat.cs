using delphibackend.Delphi.Domain.Model.Aggregates;

namespace delphibackend.Delphi.Domain.Model.Entities;

public class Chat
{
    public Guid Id { get; private set; }
    public bool isActivated { get; set; }

    // Relación con Room
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public Chat()
    {
        Id = Guid.NewGuid();
    }
}