using delphibackend.User.Domain.Model.Commands.Participant;
using delphibackend.User.Interfaces.Resources;

namespace delphibackend.User.Interfaces.Transform.Participant;

public static class UpdateParticipantCommandFromResourceAssembler
{
    public static UpdateParticipantCommand ToCommand(UpdateParticipantResource resource)
    {
        return new UpdateParticipantCommand(
            resource.Id,
            resource.AuthUserId,
            resource.Role,
            resource.IsAnonymous,
            resource.IsActive
        );
    }
}