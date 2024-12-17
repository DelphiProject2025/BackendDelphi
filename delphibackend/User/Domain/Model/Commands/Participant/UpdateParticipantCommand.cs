using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.User.Domain.Model.Commands.Participant;

public record UpdateParticipantCommand(
    Guid Id,
    Guid AuthUserId,
    ParticipantRole Role,
    bool IsAnonymous,
    bool IsActive
);