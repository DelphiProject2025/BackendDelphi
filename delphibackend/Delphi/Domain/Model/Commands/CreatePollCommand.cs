namespace delphibackend.Delphi.Domain.Model.Commands;

public record CreatePollCommand(Guid RoomId, Guid HostId, string Question, List<string> Options);
