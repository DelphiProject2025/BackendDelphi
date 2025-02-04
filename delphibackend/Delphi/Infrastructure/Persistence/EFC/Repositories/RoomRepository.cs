using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using delphibackend.User.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace delphibackend.Delphi.Infrastructure.Persistence.EFC.Repositories;

public class RoomRepository(AppDbContext context) : BaseRepository<Room>(context), IRoomRepository
{
    // Find Room by Id
    public new async Task<Room?> FindByIdAsync(Guid roomId) =>
        await context.Set<Room>().FirstOrDefaultAsync(r => r.Id == roomId);
    // Find Room by Name
    public async Task<Room?> FindByNameAsync(string roomName)
    {
        return await Context.Rooms
            .FirstOrDefaultAsync(r => r.RoomName == roomName);
    }

    public async Task<Room?> FindByPasswordAsync(string password)
    {
        return await Context.Rooms
            .FirstOrDefaultAsync(r => r.Password == password);
    }

    public async Task<(byte[]?, IReadOnlyList<Question>?)> FindSharedFileQuestionsAsync(Guid id)
    {
        var room = await Context.Set<Room>()
            .Include(r => r.Questions)  // Incluir las preguntas relacionadas
            .FirstOrDefaultAsync(r => r.Id == id);

        if (room == null)
        {
            return (null, new List<Question>().AsReadOnly());
        }

        // Cargar la referencia del archivo compartido si es necesario
        await Context.Entry(room)
            .Reference(r => r.SharedFile)
            .LoadAsync();

        return (room.SharedFile?.Content, room.Questions);
    }

    // Get Room with Users
    public async Task<Room?> GetRoomWithHostsAsync(Guid roomId)
    {
        return await Context.Rooms
            .Include(r => r.Host)
            .FirstOrDefaultAsync(r => r.Id == roomId);
    }

    // Get Participants by Room Id
    public async Task<IEnumerable<Participant>> GetParticipantsByRoomIdAsync(Guid roomId)
    {
        return await Context.Rooms
            .Where(r => r.Id == roomId)
            .SelectMany(r => r.Participants)
            .ToListAsync();
    }

    // Save changes to the database
    public async Task SaveAsync()
    {
        await Context.SaveChangesAsync();
    }
}
