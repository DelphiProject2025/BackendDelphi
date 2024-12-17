namespace delphibackend.Delphi.Domain.Model.Commands;

public record StartRecordingCommand(Guid RoomId, Guid HostId, string RecordingUrl);
