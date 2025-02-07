using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.User.Domain.Services
{
    public interface IParticipantCommandService
    {
        Task<Participant> CreateParticipantAsync(Guid authUserId, ParticipantRole role, bool isAnonymous);
        Task<bool> UpdateParticipantRoleAsync(Guid participantId, ParticipantRole newRole);
        Task<bool> DeactivateParticipantAsync(Guid participantId);
        Task<bool> DeleteParticipantAsync(Guid participantId);
        Task<bool> ActivateParticipantAsync(Guid participantId);
        Task<bool> ActivateAnonymousAsync(Guid participantId);
        Task<bool> DeactivateAnonymousAsync(Guid participantId);
        Task<bool> UpdateParticipantAsync(Participant participant);

    }
}

