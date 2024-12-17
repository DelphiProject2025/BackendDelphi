using delphibackend.Shared.Domain.Repositories;
using delphibackend.User.Domain.Model.Entities;

public interface IParticipantRepository : IBaseRepository<Participant>
{
    Task<Participant?> FindByAuthUserIdAsync(Guid authUserId);
    Task<Participant?> FindByIdAsync(Guid participantId);
    Task<IEnumerable<Participant>> FindByRoleAsync(ParticipantRole role);
    Task<IEnumerable<Participant>> GetActiveParticipantsAsync();
    Task<IEnumerable<Participant>> GetAnonymousParticipantsAsync();
    Task DeleteAsync(Guid participantId);
    Task UpdateAsync(Participant participant);
    Task<bool> ExistsByAuthUserIdAsync(Guid authUserId);
    Task<int> CountAsync();
    Task<IEnumerable<Participant>> GetActiveParticipantsByRoleAsync(ParticipantRole role);
    Task<IEnumerable<Participant>> GetRecentParticipantsAsync(DateTime since);
}