using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Queries;

namespace delphibackend.Delphi.Domain.Services;

public interface ISessionRecordingQueryService
{
    Task<IEnumerable<SessionRecording>> GetSessionRecordingsByRoomIdAsync(Guid roomId);
    Task<SessionRecording?> GetSessionRecordingByIdAsync(Guid sessionRecordingId);
    Task<IEnumerable<SessionRecording>> Handle(GetSessionRecordingsByRoomIdQuery query);
    Task<SessionRecording?> Handle(GetSessionRecordingByIdQuery query);
}