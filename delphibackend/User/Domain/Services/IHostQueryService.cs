using delphibackend.User.Domain.Model.Entities;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.User.Domain.Services
{
    public interface IHostQueryService
    {
        Task<IEnumerable<Host>> GetAllHostsAsync();
        Task<Host> GetHostByIdAsync(Guid hostId);
        Task<bool> ExistsByAuthUserIdAsync(Guid authUserId);
        Task<bool> ExistsHostByAuthUserIdAsync(Guid authUserId);

    }
}