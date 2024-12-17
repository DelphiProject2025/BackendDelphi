using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.User.Domain.Model.Queries.Participant;

public record GetParticipantsByRoleQuery(ParticipantRole Role);