using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.Delphi.Interfaces.Resources;

public record RoomResource(
    Guid Id,
    string RoomName,
    Guid HostId,
    IReadOnlyList<Question> Questions,
    ICollection<Participant> ParticipantsIds,
    Guid? SharedFileId,
    Chat Chat,
    bool Roomstarted,
    string password
    );
