using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Shared.Domain.Repositories;
using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.Delphi.Domain.Repositories;

public interface IRoomRepository : IBaseRepository<Room>
{
    Task<Room?> FindByIdAsync(Guid id);
    Task<Room?> FindByNameAsync(string roomName);
    Task<Room?> GetRoomWithHostsAsync(Guid roomId);

    Task SaveAsync();
}