
using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Interfaces.Resources;

namespace delphibackend.Delphi.Interfaces.Transform;

public static class RoomResourceFromEntityAssembler
{
    public static RoomResource ToResourceFromEntity(Room room)
    {
        return new RoomResource(
            room.Id,
            room.RoomName,
            room.HostId,
            room.Questions,
            room.Participants,
            room.SharedFileId,
            room.Chat,
            room.Roomstarted,
            room.Password);

    }
}