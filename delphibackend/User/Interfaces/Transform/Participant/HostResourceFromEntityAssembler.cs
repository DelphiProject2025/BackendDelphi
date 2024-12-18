using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Interfaces.Resources;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.User.Interfaces.Transform
{
    public static class HostResourceFromEntityAssembler
    {
        public static HostResource ToResource(Host host)
        {
            return new HostResource
            {
                Id = host.Id,
                AuthUserId = host.AuthUserId,
                CreatedAt = host.CreatedAt,
                IsActive = host.IsActive
            };
        }
    }
}