using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Domain.Services;

namespace delphibackend.User.Application.Internal.QueryServices
{
    public class ParticipantQueryService : IParticipantQueryService
    {
        private readonly IParticipantRepository _participantRepository;

        public ParticipantQueryService(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public async Task<IEnumerable<Participant>> GetAllParticipantsAsync()
        {
            return await _participantRepository.ListAsync();
        }

        public async Task<Participant?> GetParticipantByIdAsync(Guid participantId)
        {
            return await _participantRepository.FindByIdAsync(participantId);
        }

        public async Task<IEnumerable<Participant>> GetParticipantsByRoleAsync(ParticipantRole role)
        {
            return await _participantRepository.FindByRoleAsync(role);
        }

        public async Task<Participant?> GetParticipantByAuthUserIdAsync(Guid authUserId)
        {
            return await _participantRepository.FindByAuthUserIdAsync(authUserId);
        }

        public async Task<IEnumerable<Participant>> GetActiveParticipantsAsync()
        {
            // Usamos el nuevo método del repositorio
            return await _participantRepository.GetActiveParticipantsAsync();
        }

        public async Task<IEnumerable<Participant>> GetAnonymousParticipantsAsync()
        {
            return await _participantRepository.GetAnonymousParticipantsAsync();
        }

        public async Task<bool> IsUserAnActiveParticipantAsync(Guid authUserId)
        {
            var participant = await _participantRepository.FindByAuthUserIdAsync(authUserId);
            return participant?.IsActive ?? false;
        }
    }
}