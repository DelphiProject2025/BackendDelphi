using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Shared.Domain.Repositories;

namespace delphibackend.Delphi.Domain.Model.Repositories;

public interface IChatRepository : IBaseRepository<Chat>
{
    Task<Chat?> GetByRoomIdAsync(Guid roomId);
}