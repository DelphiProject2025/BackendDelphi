using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Delphi.Domain.Services;

namespace delphibackend.Delphi.Application.Internal.QueryServices
{
    public class SharedFileQueryService : ISharedFileQueryService
    {
        private readonly ISharedFileRepository _sharedFileRepository;
        private readonly IRoomRepository _roomRepository; // Se agrega esta dependencia

        public SharedFileQueryService(ISharedFileRepository sharedFileRepository, IRoomRepository roomRepository)
        {
            _sharedFileRepository = sharedFileRepository;
            _roomRepository = roomRepository;
        }

        public async Task<byte[]?> Handle(GetSharedFileByRoomIdQuery query)
        {
            var sharedFile = await _sharedFileRepository.GetSharedFileByIdAsync(query.RoomId);
            return sharedFile?.Content;
        }

        public async Task<SharedFile?> Handle(GetSharedContentByIdQuery query)
        {
            return await _sharedFileRepository.GetSharedFileByIdAsync(query.Id);
        }

        public async Task<SharedFile?> Handle(GetSharedContentDetailsQuery query)
        {
            return await _sharedFileRepository.GetSharedFileByIdAsync(query.RoomId);
        }

        public async Task<(byte[]?, IReadOnlyList<Question>?)> Handle(GetSharedFileWithQuestionsByRoomIdQuery query)
        {
            return await _roomRepository.FindSharedFileQuestionsAsync(query.RoomId);
        }
    }
}