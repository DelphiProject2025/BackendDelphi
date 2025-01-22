using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.Delphi.Domain.Services;

public interface IRoomQueryService
{
    Task<Room?> Handle(GetRoomByIdQuery query);
    Task<IEnumerable<Room>> Handle(GetAllRoomsQuery query);
    Task<Room?> Handle(GetRoomByNameQuery query);
    Task<IEnumerable<Participant>> Handle(GetParticipantsByRoomIdQuery query);
   
}