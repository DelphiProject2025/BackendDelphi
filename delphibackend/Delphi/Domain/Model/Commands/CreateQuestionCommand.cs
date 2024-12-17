namespace delphibackend.Delphi.Domain.Model.Commands;

public record CreateQuestionCommand(Guid ParticipantId,Guid RoomId,string text);