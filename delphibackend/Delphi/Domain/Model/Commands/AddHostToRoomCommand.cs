namespace delphibackend.Delphi.Domain.Model.Commands;

public record AddHostToRoomCommand(Guid RoomId,Guid HostId);