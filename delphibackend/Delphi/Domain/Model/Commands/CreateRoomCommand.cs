namespace delphibackend.Delphi.Domain.Model.Commands;

public record CreateRoomCommand(string RoomName,Guid HostId);