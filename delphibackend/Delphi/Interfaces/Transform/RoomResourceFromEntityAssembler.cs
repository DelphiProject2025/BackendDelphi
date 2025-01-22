
using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Interfaces.Resources;

namespace delphibackend.Delphi.Interfaces.Transform;

public static class RoomResourceFromEntityAssembler
{
    public static RoomResource ToResourceFromEntity(Domain.Model.Aggregates.Room room)
    {
        return new RoomResource(
            room.Id,
            room.RoomName,
            room.HostId,
            room.SharedFile,
            room.Chat,
            room.Roomstarted);

    }
}