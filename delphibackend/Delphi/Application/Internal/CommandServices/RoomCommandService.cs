using System.Security.Claims;
using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Delphi.Domain.Services;
using delphibackend.IAM.Domain.Repositories;
using delphibackend.Shared.Domain.Repositories;
using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Domain.Repositories;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.Delphi.Application.Internal.CommandServices;

public class RoomCommandService(
    IRoomRepository _roomRepository,
    IAuthUserRepository _authUserRepository,
    IHostRepository _hostRepository,
    IParticipantRepository _participantRepository,
    IHttpContextAccessor _httpContextAccessor, // <- Inyectar HttpContext

    
    IUnitOfWork unitOfWork
    ) : IRoomCommandService
{
    private Guid GetAuthenticatedUserId()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            Console.WriteLine("⚠️ No se encontró el HttpContext.");
            throw new UnauthorizedAccessException("No se encontró el contexto HTTP.");
        }

        Console.WriteLine("🔹 HttpContext encontrado.");

        foreach (var claim in httpContext.User.Claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
        }

        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.Sid);
        if (userIdClaim == null)
        {
            Console.WriteLine("⚠️ No se encontró el Claim NameIdentifier.");
            throw new UnauthorizedAccessException("Token inválido o expirado.");
        }

        Console.WriteLine($"✅ UserId obtenido: {userIdClaim.Value}");
        return Guid.Parse(userIdClaim.Value);
    }

    public async Task<Room?> Handle(CreateRoomCommand command)
    {
        var authUserId = GetAuthenticatedUserId(); // Obtiene el ID del usuario autenticado

        // Busca si el usuario ya tiene un Host asignado
        var host = await _hostRepository.FindByAuthUserIdAsync(authUserId);

        // Si el Host no existe, lo crea automáticamente
        if (host == null)
        {
            host = new Host() { AuthUserId = authUserId, IsActive = true, CreatedAt = DateTime.UtcNow };
            await _hostRepository.AddAsync(host);
            await unitOfWork.CompleteAsync();
        }

        // Crear la Room asociada al Host
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

    public async Task<Room?> Handle(AddParticipantToRoomCommand command)
    {
        // Buscar la sala
        var room = await _roomRepository.FindByPasswordAsync(command.password);
        if (room == null)
            throw new KeyNotFoundException("Room not found");

        // Buscar el participante por ID
        var participant = await _participantRepository.FindByIdAsync(command.ParticipantId);
        if (participant == null)
            throw new KeyNotFoundException("Participant not found");

        // Verificar si el participante ya existe en la sala
        if (room.Participants.Any(p => p.Id == participant.Id))
            throw new InvalidOperationException("Participant is already in the room.");

        // Asociar el participante existente a la sala
        room.Participants.Add(participant);

        // Guardar cambios
        await _roomRepository.UpdateAsync(room);
        await unitOfWork.CompleteAsync();
        
        return room;
    }


    public async Task<bool> Handle(EndRoomCommand command)
    {
        var room = await _roomRepository.FindByIdAsync(command.RoomId);
        if (room == null)
            throw new Exception("Room not found");

        // Finaliza la sala
        room.EndRoom();

        // Elimina la sala del repositorio
        await _roomRepository.DeleteAsync(room.Id);
        await unitOfWork.CompleteAsync();

        return true; // Indica que la operación fue exitosa
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
