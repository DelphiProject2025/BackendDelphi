namespace delphibackend.Delphi.Domain.Model.Commands;

public record UploadFileCommand(Guid RoomId,byte[] Content);