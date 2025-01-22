using delphibackend.Delphi.Domain.Model.Entities;

namespace delphibackend.Delphi.Interfaces.Resources;

public record RoomResource(
    Guid Id,
    string RoomName,
    Guid HostId,
    SharedFile? Chat,
    Chat SharedFile,
    bool Roomstarted
    );
