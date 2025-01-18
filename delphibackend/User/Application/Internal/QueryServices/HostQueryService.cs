using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Domain.Repositories;
using delphibackend.User.Domain.Services; // Asegúrate de incluir la interfaz IHostQueryService
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.User.Application.Internal.QueryServices
{
    public class HostQueryService : IHostQueryService
    {
        private readonly IHostRepository _hostRepository;

        public HostQueryService(IHostRepository hostRepository)
        {
            _hostRepository = hostRepository;
        }

        // Obtener todos los Hosts
        public async Task<IEnumerable<Host>> GetAllHostsAsync()
        {
            return await _hostRepository.ListAsync();
        }

        // Obtener un Host por su ID
        public async Task<Host?> GetHostByIdAsync(Guid id)
        {
            return await _hostRepository.FindByIdAsync(id);
        }

        // Obtener un Host por AuthUserId
        public async Task<Host?> GetHostByAuthUserIdAsync(Guid authUserId)
        {
            return await _hostRepository.FindByAuthUserIdAsync(authUserId);
        }
        // Verificar si existe un Host por AuthUserId
        public async Task<bool> ExistsByAuthUserIdAsync(Guid authUserId)
        {
            return await _hostRepository.ExistsByAuthUserIdAsync(authUserId);
        }


        // Verificar si existe un Host por AuthUserId
        public async Task<bool> ExistsHostByAuthUserIdAsync(Guid authUserId)
        {
            return await _hostRepository.ExistsByAuthUserIdAsync(authUserId);
        }
    }
}