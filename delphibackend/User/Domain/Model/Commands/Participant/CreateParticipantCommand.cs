using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.User.Domain.Model.Commands.Participant;

public record CreateParticipantCommand(
    Guid AuthUserId,
    ParticipantRole Role = ParticipantRole.Contributor,
    bool IsAnonymous = false,
    bool IsActive = true
);