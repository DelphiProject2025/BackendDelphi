using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Interfaces.Resources;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.User.Interfaces.Transform
{
    public static class UpdateHostCommandFromResourceAssembler
    {
        public static void ApplyUpdate(Host host, UpdateHostResource resource)
        {
            host.IsActive = resource.IsActive; // Actualiza el estado activo/inactivo
        }
    }
}