namespace delphibackend.Delphi.Domain.Model.Commands;

public record AddParticipantToRoomCommand(string password,Guid ParticipantId);