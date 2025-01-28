using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Shared.Domain.Repositories;


namespace delphibackend.Delphi.Domain.Repositories
{
    public interface ISessionRecordingRepository : IBaseRepository<SessionRecording>
    {
        Task<IEnumerable<SessionRecording>> GetSessionRecordingsByRoomIdAsync(Guid roomId);
        Task<SessionRecording?> GetByIdAsync(Guid sessionRecordingId);
    }
}