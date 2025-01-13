using delphibackend.User.Domain.Model.Entities;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.User.Domain.Services
{
    public interface IHostCommandService
    {
        Task<Host> CreateHostAsync(Guid authUserId);
        Task<bool> UpdateHostAsync(Host host);
        Task<bool> DeleteHostAsync(Guid hostId);
        Task<bool> ActivateHostAsync(Guid hostId);
        Task<bool> DeactivateHostAsync(Guid hostId);
    }
}