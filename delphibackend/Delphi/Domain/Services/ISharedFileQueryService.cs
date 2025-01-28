using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Queries;

namespace delphibackend.Delphi.Domain.Services
{
    public interface ISharedFileQueryService
    {
        Task<byte[]?> Handle(GetSharedFileByRoomIdQuery query);
        Task<SharedFile?> Handle(GetSharedContentByIdQuery query);
        Task<SharedFile?> Handle(GetSharedContentDetailsQuery query);
        Task<(byte[]?, IReadOnlyList<Question>?)> Handle(GetSharedFileWithQuestionsByRoomIdQuery query);
    }
    }
