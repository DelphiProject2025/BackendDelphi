using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Interfaces.Resources;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.User.Interfaces.Transform
{
    public static class CreateHostCommandFromResourceAssembler
    {
        public static Host ToEntity(CreateHostResource resource)
        {
            return new Host
            {
                AuthUserId = resource.AuthUserId,
                IsActive = true, // Por defecto activo al crearse
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}