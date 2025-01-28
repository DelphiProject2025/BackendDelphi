using delphibackend.Delphi.Application.Internal.QueryServices;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Delphi.Domain.Services;

namespace delphibackend.Delphi.Application.Internal.QueryServices;

public class SessionRecordingQueryService : ISessionRecordingQueryService
{
    private readonly ISessionRecordingRepository _sessionRecordingRepository;

    public SessionRecordingQueryService(ISessionRecordingRepository sessionRecordingRepository)
    {
        _sessionRecordingRepository = sessionRecordingRepository;
    }

    public async Task<IEnumerable<SessionRecording>> GetSessionRecordingsByRoomIdAsync(Guid roomId)
    {
        return await _sessionRecordingRepository.GetSessionRecordingsByRoomIdAsync(roomId);
    }

    public async Task<SessionRecording?> GetSessionRecordingByIdAsync(Guid sessionRecordingId)
    {
        return await _sessionRecordingRepository.GetByIdAsync(sessionRecordingId);
    }

    public async Task<IEnumerable<SessionRecording>> Handle(GetSessionRecordingsByRoomIdQuery query)
    {
        return await GetSessionRecordingsByRoomIdAsync(query.RoomId);
    }

    public async Task<SessionRecording?> Handle(GetSessionRecordingByIdQuery query)
    {
        return await GetSessionRecordingByIdAsync(query.SessionRecordingId);
    }
}
