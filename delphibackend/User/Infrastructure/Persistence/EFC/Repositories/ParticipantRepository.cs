using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using delphibackend.User.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
{
    public ParticipantRepository(AppDbContext context) : base(context) { }

    public async Task<Participant?> FindByAuthUserIdAsync(Guid authUserId)
    {
        return await Context.Set<Participant>().FirstOrDefaultAsync(p => p.AuthUserId == authUserId);
    }

    public new async Task<Participant?> FindByIdAsync(Guid participantId)
    {
        try
        {
            return await Context.Set<Participant>().FirstOrDefaultAsync(p => p.Id == participantId);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<Participant>> FindByRoleAsync(ParticipantRole role)
    {
        return await Context.Set<Participant>().Where(p => p.Role == role).ToListAsync();
    }

    public async Task<IEnumerable<Participant>> GetActiveParticipantsAsync()
    {
        return await Context.Set<Participant>().Where(p => p.IsActive).ToListAsync();
    }

    public async Task<IEnumerable<Participant>> GetAnonymousParticipantsAsync()
    {
        return await Context.Set<Participant>().Where(p => p.IsAnonymous).ToListAsync();
    }

    public async Task DeleteAsync(Guid participantId)
    {
        var participant = await FindByIdAsync(participantId);
        if (participant != null)
        {
            Context.Set<Participant>().Remove(participant);
            await Context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Participant participant)
    {
        Context.Set<Participant>().Update(participant);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByAuthUserIdAsync(Guid authUserId)
    {
        return await Context.Set<Participant>().AnyAsync(p => p.AuthUserId == authUserId);
    }

    public async Task<int> CountAsync()
    {
        return await Context.Set<Participant>().CountAsync();
    }

    public async Task<IEnumerable<Participant>> GetActiveParticipantsByRoleAsync(ParticipantRole role)
    {
        return await Context.Set<Participant>().Where(p => p.IsActive && p.Role == role).ToListAsync();
    }

    public async Task<IEnumerable<Participant>> GetRecentParticipantsAsync(DateTime since)
    {
        return await Context.Set<Participant>().Where(p => p.JoinedAt >= since).ToListAsync();
    }
}
