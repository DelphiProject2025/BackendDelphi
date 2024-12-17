using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Domain.Services;
using delphibackend.Shared.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace delphibackend.User.Application.Internal.CommandServices
{
    public class ParticipantCommandService : IParticipantCommandService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ParticipantCommandService(IParticipantRepository participantRepository, IUnitOfWork unitOfWork)
        {
            _participantRepository = participantRepository;
            _unitOfWork = unitOfWork;
        }

        // Método para activar participante anónimo
        public async Task<bool> ActivateAnonymousAsync(Guid participantId)
        {
            var participant = await _participantRepository.FindByIdAsync(participantId);
            if (participant == null || participant.IsAnonymous) 
                return false;

            participant.IsAnonymous = true; // Cambiar IsAnonymous a true
            await _unitOfWork.CompleteAsync();
            return true;
        }

        // Método para desactivar participante anónimo
        public async Task<bool> DeactivateAnonymousAsync(Guid participantId)
        {
            var participant = await _participantRepository.FindByIdAsync(participantId);
            if (participant == null || !participant.IsAnonymous)
                return false;

            participant.IsAnonymous = false; // Desactivar el participante
            await _unitOfWork.CompleteAsync();
            return true;
        }

        // Otros métodos existentes
        public async Task<Participant?> CreateParticipantAsync(Guid authUserId, ParticipantRole role, bool isAnonymous)
        {
            var participant = new Participant
            {
                AuthUserId = authUserId,
                Role = role,
                IsAnonymous = isAnonymous,
                IsActive = true,
                JoinedAt = DateTime.UtcNow
            };

            await _participantRepository.AddAsync(participant);
            await _unitOfWork.CompleteAsync();
            return participant;
        }

        public async Task<bool> UpdateParticipantRoleAsync(Guid participantId, ParticipantRole newRole)
        {
            var participant = await _participantRepository.FindByIdAsync(participantId);
            if (participant == null) return false;

            participant.Role = newRole;
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteParticipantAsync(Guid participantId)
        {
            await _participantRepository.DeleteAsync(participantId);
            return true;
        }

        public async Task<bool> DeactivateParticipantAsync(Guid participantId)
        {
            var participant = await _participantRepository.FindByIdAsync(participantId);
            if (participant == null) return false;

            participant.IsActive = false;
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> ActivateParticipantAsync(Guid participantId)
        {
            var participant = await _participantRepository.FindByIdAsync(participantId);
            if (participant == null) return false;

            participant.IsActive = true;
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
