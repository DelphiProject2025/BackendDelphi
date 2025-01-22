using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Delphi.Domain.Services;
using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.Delphi.Application.Internal.Queryservices;

public class RoomQueryService(IRoomRepository roomRepository) : IRoomQueryService
{
    private readonly IRoomRepository _roomRepository;

    public async Task<Room?> Handle(GetRoomByIdQuery query)
    {
        return await roomRepository.FindByIdAsync(query.RoomId);
    }
    public async Task<IEnumerable<Room>> Handle(GetAllRoomsQuery query)
    {
        return await roomRepository.ListAsync();
    }
    public async Task<Room?> Handle(GetRoomByNameQuery query)
    {
        return await _roomRepository.FindByNameAsync(query.RoomName);
    }

    public Task<IEnumerable<Participant>> Handle(GetParticipantsByRoomIdQuery query)
    {
        throw new NotImplementedException();
    }


    public async Task SaveChangesAsync()
    {
        await _roomRepository.SaveAsync();
    }
}