using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.User.Interfaces.Resources
{
    public class CreateParticipantResource
    {
        public Guid AuthUserId { get; set; }
        public ParticipantRole Role { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsActive { get; set; }
    }
}