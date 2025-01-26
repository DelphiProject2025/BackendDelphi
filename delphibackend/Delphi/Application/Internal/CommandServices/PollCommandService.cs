using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Delphi.Domain.Services;
using delphibackend.User.Domain.Repositories;

namespace delphibackend.Delphi.Application.Internal.CommandServices;

public class PollCommandService : IPollCommandService
{
    private readonly IPollRepository _pollRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IHostRepository _hostRepository;

    public PollCommandService(IPollRepository pollRepository, IRoomRepository roomRepository, IHostRepository hostRepository)
    {
        _pollRepository = pollRepository;
        _roomRepository = roomRepository;
        _hostRepository = hostRepository;
    }

    public async Task<Guid> Handle(CreatePollCommand command)
    {
        var room = await _roomRepository.FindByIdAsync(command.RoomId);
        var host = await _hostRepository.FindByIdAsync(command.HostId);

        if (room == null || host == null)
        {
            throw new InvalidOperationException("Room or Host not found.");
        }

        var poll = new Poll(room, host, command.Question, command.Options);
        await _pollRepository.AddAsync(poll);

        return poll.Id;
    }

    public async Task Handle(ClosePollCommand command)
    {
        var poll = await _pollRepository.FindByIdAsync(command.PollId);
        if (poll == null)
        {
            throw new InvalidOperationException("Poll not found.");
        }

        poll.ClosePoll();
        await _pollRepository.UpdateAsync(poll);
    }

    public async Task Handle(VotePollOptionCommand command)
    {
        var poll = await _pollRepository.FindByIdAsync(command.PollId);
        if (poll == null)
        {
            throw new InvalidOperationException("Poll not found.");
        }

        poll.Vote(command.PollOptionId);
        await _pollRepository.UpdateAsync(poll);
    }
}