using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Interfaces.Resources;

namespace delphibackend.Delphi.Interfaces.Transform;

public class AddParticipantToRoomCommandFromResourceAssembler
{
    public static AddParticipantToRoomCommand ToCommandFromResource(AddParticipantToRoomResource addParticipantToRoomResource, Guid hostId)
    {
        return new AddParticipantToRoomCommand(addParticipantToRoomResource.password, hostId);
    }
}