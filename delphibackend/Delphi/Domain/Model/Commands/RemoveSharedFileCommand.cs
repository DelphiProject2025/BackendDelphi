namespace delphibackend.Delphi.Domain.Model.Commands;

public record RemoveSharedFileCommand(Guid RoomId, Guid FileId);
