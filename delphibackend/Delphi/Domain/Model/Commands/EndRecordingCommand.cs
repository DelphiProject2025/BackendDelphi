namespace delphibackend.Delphi.Domain.Model.Commands;

public record EndRecordingCommand(Guid RecordingId, DateTime EndTime, long FileSize);
