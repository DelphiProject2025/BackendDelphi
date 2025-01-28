using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Queries;

namespace delphibackend.Delphi.Domain.Services
{
    public interface ISharedFileQueryService
    {
        Task<(byte[]?, IReadOnlyList<Question?>)> Handle(GetSharedFileByRoomIdQuery query);
    }
}