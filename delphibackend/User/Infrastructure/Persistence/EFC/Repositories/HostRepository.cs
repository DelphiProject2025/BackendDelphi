using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.User.Infrastructure.Repositories
{
    public class HostRepository : IHostRepository
    {
        private readonly AppDbContext _context;

        public HostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Host>> ListAsync()
        {
            return await _context.Host.ToListAsync();
        }

        public async Task<Host?> FindByIdAsync(Guid id)
        {
            return await _context.Host.FindAsync(id);
        }

        public async Task<Host?> FindByAuthUserIdAsync(Guid authUserId)
        {
            return await _context.Host
                .FirstOrDefaultAsync(h => h.AuthUserId == authUserId);
        }

        public async Task<bool> ExistsByAuthUserIdAsync(Guid authUserId)
        {
            return await _context.Host
                .AnyAsync(h => h.AuthUserId == authUserId);
        }
        public async Task DeleteAsync(Guid id)
        {
            var host = await _context.Host.FindAsync(id);
            if (host != null)
            {
                _context.Host.Remove(host);
            }
        }
        public async Task UpdateAsync(Host host)
        {
            _context.Host.Update(host);
        }



        public async Task AddAsync(Host host)
        {
            await _context.Host.AddAsync(host); 
            await _context.SaveChangesAsync(); 
        }
        public void Update(Host host)
        {
            _context.Host.Update(host);
        }

        public void Delete(Guid id)
        {
            var host = new Host { Id = id };
            _context.Host.Remove(host);
        }
    }
}