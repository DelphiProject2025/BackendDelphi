using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.User.Infrastructure.Persistence.EFC.Repositories;

public class HostRepository : IHostRepository
{
    private readonly AppDbContext _context;

    public HostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Host>> ListAsync()
    {
        return await _context.Set<Host>().ToListAsync();
    }

    public async Task<Host?> FindByIdAsync(Guid id)
    {
        return await _context.Set<Host>().FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task AddAsync(Host host)
    {
        await _context.Set<Host>().AddAsync(host);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Host host)
    {
        _context.Set<Host>().Update(host);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var host = await FindByIdAsync(id);
        if (host != null)
        {
            _context.Set<Host>().Remove(host);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByAuthUserIdAsync(Guid authUserId)
    {
        return await _context.Set<Host>().AnyAsync(h => h.AuthUserId == authUserId);
    }
}