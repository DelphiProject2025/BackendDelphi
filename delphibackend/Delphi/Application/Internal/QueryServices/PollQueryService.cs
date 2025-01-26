using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Delphi.Domain.Services;
using delphibackend.Shared.Domain.Repositories;

namespace delphibackend.Delphi.Application.Internal.QueryServices;

public class PollQueryService : IPollQueryService
{
    private readonly IPollRepository _pollRepository;

    public PollQueryService(IPollRepository pollRepository)
    {
        _pollRepository = pollRepository ?? throw new ArgumentNullException(nameof(pollRepository));
    }
    public async Task<Poll?> GetPollByIdAsync(Guid pollId)
    {
        return await _pollRepository.FindByIdAsync(pollId);
    }

    public async Task<IEnumerable<Poll>> GetPollsByRoomIdAsync(Guid roomId)
    {
        return await _pollRepository.GetPollsByRoomIdAsync(roomId);
    }
}