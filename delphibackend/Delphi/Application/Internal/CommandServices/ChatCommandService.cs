using delphibackend.Delphi.Domain.Model.Repositories;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Delphi.Domain.Services;

namespace delphibackend.Delphi.Application.Internal.CommandServices
{
    public class ChatCommandService : IChatCommandService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IChatRepository _chatRepository;

        public ChatCommandService(IRoomRepository roomRepository, IChatRepository chatRepository)
        {
            _roomRepository = roomRepository;
            _chatRepository = chatRepository;
        }
    }
}