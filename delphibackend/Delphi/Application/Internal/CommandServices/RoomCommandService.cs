using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Delphi.Domain.Services;
using delphibackend.IAM.Domain.Repositories;
using delphibackend.Shared.Domain.Repositories;
using delphibackend.User.Domain.Repositories;

namespace delphibackend.Delphi.Application.Internal.CommandServices;

public class RoomCommandService(
    IRoomRepository _roomRepository,
    IAuthUserRepository _authUserRepository,
    IHostRepository _hostRepository,
    
    IUnitOfWork unitOfWork
    ) : IRoomCommandService
{
    public async Task<Room?> Handle(CreateRoomCommand command)
    {
        // Usa HostRepository para buscar el host
        var host = await _hostRepository.FindByIdAsync(command.HostId);
        if (host == null)
        {
            throw new Exception("Host not found");
        }

        var room = new Room(command.RoomName, host);
        await _roomRepository.AddAsync(room);
        await unitOfWork.CompleteAsync();
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

    public Task<Room?> Handle(AddParticipantToRoomCommand command)
    {
        throw new NotImplementedException();
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
    public async Task<Room?> Handle(AddHostToRoomCommand command)
    {
        // Buscar la sala
        var room = await _roomRepository.FindByIdAsync(command.RoomId);
        if (room == null)
            throw new Exception("Room not found");

        // Buscar el usuario autenticado que será el Host
        var authUser = await _authUserRepository.FindByIdAsync(command.HostId);
        if (authUser == null)
            throw new Exception("AuthUser not found");

        // Crear una nueva instancia de Host usando el constructor
        var host = new delphibackend.User.Domain.Model.Entities.Host(authUser);

        // Asignar el Host a la sala
        room.Host = host;

        // Guardar cambios
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
