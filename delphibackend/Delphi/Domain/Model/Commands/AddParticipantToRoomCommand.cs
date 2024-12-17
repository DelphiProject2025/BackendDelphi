namespace delphibackend.Delphi.Domain.Model.Commands;

public record AddParticipantToRoomCommand(Guid RoomId,Guid ParticipantId);