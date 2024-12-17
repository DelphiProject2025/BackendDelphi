using delphibackend.User.Domain.Model.Entities;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.User.Domain.Repositories
{
    public interface IHostRepository
    {
        Task<IEnumerable<Host>> ListAsync(); // Obtener todos los Hosts
        Task<Host?> FindByIdAsync(Guid id); // Encontrar un Host por su ID
        Task AddAsync(Host host); // Agregar un nuevo Host
        Task UpdateAsync(Host host); // Actualizar un Host existente
        Task DeleteAsync(Guid id); // Eliminar un Host
        Task<bool> ExistsByAuthUserIdAsync(Guid authUserId); // Verificar si ya existe un Host
    }
}