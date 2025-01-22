using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Delphi.Domain.Services;
using delphibackend.IAM.Domain.Repositories;
using delphibackend.Shared.Domain.Repositories;

namespace delphibackend.Delphi.Application.Internal.CommandServices;

public class RoomCommandService(
    IRoomRepository _roomRepository,
    IAuthUserRepository _authUserRepository,
    IUnitOfWork unitOfWork
    ) : IRoomCommandService
{
    public async Task<Room?> Handle(CreateRoomCommand command)
    {
        var host = await _authUserRepository .FindByIdAsync(command.HostId);
        if (host == null)
            throw new Exception("Host not found");

        var room = new Room(command.Name, host);
        await _roomRepository.AddAsync(room);
        await unitOfWork.CompleteAsync();
        return room;
    }

    public async Task<Room?> Handle(AddParticipantToRoomCommand command)
    {
        var room = await _roomRepository.GetRoomWithUsersAsync(command.RoomId);
        if (room == null)
            throw new Exception("Room not found");

        var participant = await _authUserRepository .FindByIdAsync(command.ParticipantId);
        if (participant == null)
            throw new Exception("Participant not found");

        room.AddParticipant(participant);
        await _roomRepository.SaveAsync();
        return room;
    }

    public async Task<Room?> Handle(StartRoomCommand command)
    {
        var room = await _roomRepository.FindByIdAsync(command.RoomId);
        if (room == null)
            throw new Exception("Room not found");

        room.StartRoom();
        await _roomRepository.UpdateAsync(room);
        await unitOfWork.CompleteAsync();
        return room;
    }

    public async Task<Room?> Handle(EndRoomCommand command)
    {
        var room = await _roomRepository.FindByIdAsync(command.RoomId);
        if (room == null)
            throw new Exception("Room not found");

        room.EndRoom();
        await _roomRepository.UpdateAsync(room);
        await unitOfWork.CompleteAsync();
        return room;
    }

    public async Task<bool> Handle(CheckIfActivatedCommand command)
    {
        var room = await _roomRepository.FindByIdAsync(command.RoomId);
        if (room == null || !room.Roomstarted)
            return false;

        return room.Chat?.isActivated ?? false;
    }

    public async Task<bool> Handle(UploadFileCommand command)
    {
        var room = await _roomRepository.FindByIdAsync(command.RoomId);
        if (room == null)
            throw new Exception("Room not found");

        room.UploadFile(command.Content);
        await _roomRepository.UpdateAsync(room);
        await unitOfWork.CompleteAsync();
        return true;
    }
}
