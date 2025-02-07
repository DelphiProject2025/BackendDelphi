using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Domain.Services;
using delphibackend.Shared.Domain.Repositories;
using System;
using System.Threading.Tasks;
using delphibackend.IAM.Domain.Repositories;

namespace delphibackend.User.Application.Internal.CommandServices
{
    public class ParticipantCommandService : IParticipantCommandService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthUserRepository _authUserRepository;

        public ParticipantCommandService(IParticipantRepository participantRepository, IUnitOfWork unitOfWork, IAuthUserRepository authUserRepository)
        {
            _participantRepository = participantRepository;
            _unitOfWork = unitOfWork;
            _authUserRepository = authUserRepository;
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
            // Buscar el AuthUser antes de crear el Participant
            var authUser = await _authUserRepository.FindByIdAsync(authUserId);
            if (authUser == null) return null; // Si no existe el usuario, retorna null

            var participant = new Participant
            {
                AuthUserId = authUserId,
                AuthUser = authUser, // 🔥 Asignar el objeto completo
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
        public async Task<bool> UpdateParticipantAsync(Participant participant)
        {
            var existingParticipant = await _participantRepository.FindByIdAsync(participant.Id);
            if (existingParticipant == null) return false;

            // Solo actualiza el displayName
            existingParticipant.DisplayName = participant.DisplayName;

            await _unitOfWork.CompleteAsync();
            return true;
        }

    }
}
