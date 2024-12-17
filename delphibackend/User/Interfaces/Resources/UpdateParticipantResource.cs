using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.User.Interfaces.Resources;

public record UpdateParticipantResource(
    Guid Id,
    Guid AuthUserId,
    ParticipantRole Role,
    bool IsAnonymous,
    bool IsActive)
{
    public ParticipantRole NewRole { get; set; }
}