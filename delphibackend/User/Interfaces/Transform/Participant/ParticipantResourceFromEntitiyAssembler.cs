using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Interfaces.Resources;

namespace delphibackend.User.Interfaces.Transform.Participant;

public static class ParticipantResourceFromEntityAssembler
{
    public static ParticipantResource ToResource(Domain.Model.Entities.Participant entity)
    {
        return new ParticipantResource(
            entity.Id,
            entity.AuthUserId,
            entity.Role.ToString(), 
            entity.IsAnonymous,
            entity.IsActive,
            entity.JoinedAt,
            entity.DisplayName
        );
    }
}