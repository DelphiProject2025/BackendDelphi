namespace delphibackend.Delphi.Domain.Model.Commands;

public record VotePollOptionCommand(Guid PollId, Guid ParticipantId, Guid PollOptionId);
