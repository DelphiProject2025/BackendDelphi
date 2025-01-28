using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Shared.Domain.Repositories;

namespace delphibackend.Delphi.Domain.Repositories;


public interface ISharedFileRepository : IBaseRepository<SharedFile>
    {
        SharedFile? FindSharedFileByIdSync(Guid fileId);
        Task<SharedFile?> GetSharedFileByIdAsync(Guid fileId);
        
    }
