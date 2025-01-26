using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Shared.Domain.Repositories;

namespace delphibackend.Delphi.Domain.Repositories
{
    public interface IPollRepository : IBaseRepository<Poll>
    {
        Task<Poll?> FindByIdAsync(Guid id);

        Task<IEnumerable<Poll>> GetPollsByRoomIdAsync(Guid roomId);
    }
}