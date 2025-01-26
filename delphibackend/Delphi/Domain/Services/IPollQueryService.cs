using delphibackend.Delphi.Domain.Model.Entities;

namespace delphibackend.Delphi.Domain.Services;

public interface IPollQueryService
{
    Task<Poll?> GetPollByIdAsync(Guid pollId);

    Task<IEnumerable<Poll>> GetPollsByRoomIdAsync(Guid roomId);
}