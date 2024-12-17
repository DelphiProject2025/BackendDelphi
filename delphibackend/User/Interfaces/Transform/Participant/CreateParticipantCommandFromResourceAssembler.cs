using delphibackend.User.Domain.Model.Commands.Participant;
using delphibackend.User.Interfaces.Resources;

namespace delphibackend.User.Interfaces.Transform.Participant;

public static class CreateParticipantCommandFromResourceAssembler
{
    public static CreateParticipantCommand Transform(CreateParticipantResource resource)
    {
        return new CreateParticipantCommand(
            resource.AuthUserId,
            resource.Role,
            resource.IsAnonymous,
            resource.IsActive
        );
    }

}