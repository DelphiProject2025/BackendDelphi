using DomainHost = delphibackend.User.Domain.Model.Entities.Host;
using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Interfaces.Resources;

namespace delphibackend.User.Interfaces.Transform.Participant
{
    public static class UpdateDisplayNameFromResourceAssembler
    {
        public static void ApplyToHost(DomainHost host, UpdateDisplayNameResource resource)
        {
            host.DisplayName = resource.DisplayName;
        }

        public static void ApplyToParticipant(Domain.Model.Entities.Participant participant, UpdateDisplayNameResource resource)
        {
            participant.DisplayName = resource.DisplayName;
        }
    }
}