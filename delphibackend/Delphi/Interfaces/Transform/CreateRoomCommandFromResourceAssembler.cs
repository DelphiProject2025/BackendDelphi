using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Interfaces.Resources;

namespace delphibackend.Delphi.Interfaces.Transform;

public class CreateRoomCommandFromResourceAssembler
{
    public static CreateRoomCommand ToCommandFromResource(CreateRoomResource resource)
    {
        return new CreateRoomCommand(resource.RoomName, resource.HostId);
    }
}