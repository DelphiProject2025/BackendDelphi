using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace delphibackend.Delphi.Infrastructure.Persistence.EFC.Repositories;

    public class SessionRecordingRepository : BaseRepository<SessionRecording>, ISessionRecordingRepository
    {
        public SessionRecordingRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<SessionRecording>> GetSessionRecordingsByRoomIdAsync(Guid roomId)
        {
            return await Context.Set<SessionRecording>()
                .Where(sr => sr.RoomId == roomId)
                .Include(sr => sr.Room) // Relación con Room
                .Include(sr => sr.Host) // Relación con Host
                .ToListAsync();
        }
        public async Task<SessionRecording?> GetByIdAsync(Guid sessionRecordingId)
        {
            return await Context.Set<SessionRecording>()
                .Include(sr => sr.Room)
                .Include(sr => sr.Host)
                .FirstOrDefaultAsync(sr => sr.Id == sessionRecordingId);
        }
    }
