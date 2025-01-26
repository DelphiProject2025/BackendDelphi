using System.Linq.Expressions;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Repositories;

using Microsoft.EntityFrameworkCore;

namespace delphibackend.Delphi.Infrastructure.Persistence.EFC.Repositories;

public class PollRepository : BaseRepository<Poll>, IPollRepository
{
    public PollRepository(AppDbContext context) : base(context) { }

    public new async Task<Poll?> FindByIdAsync(Guid id)
    {
        return await Context.Set<Poll>()
            .Include(p => p.Options) // Incluye las opciones de la encuesta
            .FirstOrDefaultAsync(p => p.Id == id);
    }


    public async Task<IEnumerable<Poll>> FindAllAsync(Expression<Func<Poll, bool>> predicate)
    {
        return await Context.Set<Poll>().Where(predicate).ToListAsync();
    }
    public async Task<IEnumerable<Poll>> GetPollsByRoomIdAsync(Guid roomId)
    {
        return await Context.Set<Poll>()
            .Where(p => p.RoomId == roomId)
            .Include(p => p.Options) // Incluye las opciones si es necesario
            .ToListAsync();
    }
}