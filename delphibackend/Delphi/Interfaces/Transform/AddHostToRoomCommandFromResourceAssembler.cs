using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Interfaces.Resources;

namespace delphibackend.Delphi.Interfaces.Transform;

public class AddHostToRoomCommandFromResourceAssembler
{
    public static AddHostToRoomCommand ToCommandFromResource(AddHostToRoomResource addHostToRoomResource, Guid hostId)
    {
        return new AddHostToRoomCommand(addHostToRoomResource.RoomId, hostId);
    }
}