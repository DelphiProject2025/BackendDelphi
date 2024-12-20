using delphibackend.Delphi.Domain.Model.Aggregates;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.Delphi.Domain.Model.Entities;

public class Poll
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public Guid HostId {get; set;}
    public Host Host { get; set; }
    
    public string Question { get; private set; } // Pregunta de la encuesta
    public List<PollOption> Options { get; private set; } = new List<PollOption>(); // Opciones de la encuesta

    public bool IsActive { get; private set; } // Indica si la encuesta está activa
    public DateTime CreatedAt { get; private set; } // Fecha y hora de creación

    public Poll(Room room, Host host, string question, List<string> options)
    {
        if (room == null)
        {
            throw new ArgumentNullException(nameof(room), "Room cannot be null.");
        }

        if (host == null)
        {
            throw new ArgumentNullException(nameof(host), "Host cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(question))
        {
            throw new ArgumentException("Poll question cannot be null or empty.", nameof(question));
        }

        if (options == null || options.Count < 2)
        {
            throw new ArgumentException("Poll must have at least two options.", nameof(options));
        }

        Id = Guid.NewGuid();
        RoomId = room.Id;
        Room = room;
        HostId = host.Id;
        Host = host;
        Question = question;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;

        foreach (var option in options)
        {
            Options.Add(new PollOption(option));
        }
    }

    private Poll() { }

    public void ClosePoll()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Poll is already closed.");
        }

        IsActive = false;
    }
    
    public void Vote(Guid optionId)
    {
        var option = Options.FirstOrDefault(o => o.Id == optionId);

        if (option == null)
        {
            throw new InvalidOperationException("Option not found.");
        }

        option.Vote();
    }

}