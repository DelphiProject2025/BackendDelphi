using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Shared.Domain.Repositories;
using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.Delphi.Domain.Repositories;

public interface IRoomRepository : IBaseRepository<Room>
{
    Task<Room?> FindByNameAsync(string roomName);
    Task<Room?> FindByPasswordAsync(string password);
    Task<Room?> GetRoomWithHostsAsync(Guid roomId);
    Task<IEnumerable<Participant>> GetParticipantsByRoomIdAsync(Guid roomId);
    public abstract Task<(byte[]?, IReadOnlyList<Question>?)> FindSharedFileQuestionsAsync(Guid id);


    Task SaveAsync();
}