using delphibackend.Delphi.Domain.Model.Entities;

namespace delphibackend.Delphi.Domain.Repositories;


    public interface ISharedFileRepository
    {
        SharedFile? FindSharedFileByIdSync(Guid fileId);
        Task<SharedFile?> GetSharedFileByIdAsync(Guid fileId);
    }
