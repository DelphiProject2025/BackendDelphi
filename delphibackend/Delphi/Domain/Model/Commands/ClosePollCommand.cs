namespace delphibackend.Delphi.Domain.Model.Commands;

public record ClosePollCommand(Guid PollId, Guid HostId);
