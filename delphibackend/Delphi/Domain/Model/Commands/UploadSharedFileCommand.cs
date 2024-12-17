namespace delphibackend.Delphi.Domain.Model.Commands;

public record UploadSharedFileCommand(Guid RoomId, string FileName, byte[] FileContent);
