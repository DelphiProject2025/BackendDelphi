namespace delphibackend.Delphi.Domain.Model.Commands;

public record CreateRoomCommand(string Name,Guid HostId);