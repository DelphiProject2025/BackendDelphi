using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Shared.Domain.Repositories;
using delphibackend.Shared.Infraestructure.Persistences.EFC;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace delphibackend.Delphi.Infrastructure.Persistence.EFC.Repositories
{
    public class SharedFileRepository : BaseRepository<SharedFile>, ISharedFileRepository
    {
        public SharedFileRepository(AppDbContext context) : base(context) { }

        public SharedFile? FindSharedFileByIdSync(Guid fileId)
        {
            return Context.Set<SharedFile>()
                .FirstOrDefault(sf => sf.Id == fileId);
        }

        public async Task<SharedFile?> GetSharedFileByIdAsync(Guid fileId)
        {
            return await Context.SharedFiles.FindAsync(fileId);
        }
    }
}