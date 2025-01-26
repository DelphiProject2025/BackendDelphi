using delphibackend.Delphi.Domain.Model.Commands;

namespace delphibackend.Delphi.Domain.Services;

public interface IPollCommandService
{
    Task<Guid> Handle(CreatePollCommand command);
    Task Handle(ClosePollCommand command);
    Task Handle(VotePollOptionCommand command);
}