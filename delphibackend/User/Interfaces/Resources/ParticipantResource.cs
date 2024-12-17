using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.User.Interfaces.Resources;

public record ParticipantResource(
    Guid Id,
    Guid AuthUserId,
    string Role, // Debe aceptar `string`.
    bool IsAnonymous,
    bool IsActive,
    DateTime JoinedAt,
    string DisplayName
);
