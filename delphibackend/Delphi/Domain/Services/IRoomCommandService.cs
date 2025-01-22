using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.Delphi.Domain.Services;

public interface IRoomCommandService
{
    Task<Room?> Handle(CreateRoomCommand command);
    Task<bool> Handle(CheckIfActivatedCommand command);
    
    Task<Room?> Handle(EndRoomCommand command);
    Task<Room?> Handle(StartRoomCommand command);
    Task<Room?> Handle(AddParticipantToRoomCommand command);

    
   
}