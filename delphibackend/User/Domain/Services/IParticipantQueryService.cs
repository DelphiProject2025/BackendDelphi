using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.User.Domain.Services
{
    public interface IParticipantQueryService
    {
        Task<IEnumerable<Participant>> GetAllParticipantsAsync();
        Task<Participant?> GetParticipantByIdAsync(Guid participantId);
        Task<IEnumerable<Participant>> GetParticipantsByRoleAsync(ParticipantRole role);
        Task<Participant?> GetParticipantByAuthUserIdAsync(Guid authUserId);
        Task<IEnumerable<Participant>> GetActiveParticipantsAsync();
        Task<IEnumerable<Participant>> GetAnonymousParticipantsAsync();
        Task<bool> IsUserAnActiveParticipantAsync(Guid authUserId);
    }
}