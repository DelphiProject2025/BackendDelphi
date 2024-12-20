using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.IAM.Domain.Model.Aggregates;
using delphibackend.User.Domain.Model.Entities;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.Delphi.Domain.Model.Aggregates;

public class Room
{
    public Guid Id { get; private set; }
    public string RoomName { get; private set; }
    public bool Roomstarted { get; set; }
    public string Password { get; private set; } // Contraseña o PIN visible para los participantes
    public Guid HostId { get; set; }
    public Host Host { get; internal set; }
    public Guid? SharedFileId {get; set;}
    public SharedFile? SharedFile {get; set;}
    public Guid? ChatId {get; set;}
    public SessionRecording? SessionRecording { get; set; }
    public Guid? SessionRecordingId { get; set; }
    public Chat Chat {get;set;}
    public ICollection<Participant> Participants { get;  set; } = new List<Participant>();
    private readonly List<Question> _questions = new List<Question>();
    public IReadOnlyList<Question> Questions => _questions.AsReadOnly();

    public ICollection<Poll> Polls { get; private set; } = new List<Poll>();

    public Room(){}
    public Room(string name, AuthUser user)
    {
        Id = Guid.NewGuid();
        RoomName = name;
        Roomstarted = false;
        Password = GenerateRandomPassword();
        Host = new Host(user.Id);
        HostId = Host.Id;
        SharedFile = new SharedFile();
        Chat = new Chat();

    }
    
    private string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
    }
    
    public void UploadFile(byte[] content)
    {
        if (SharedFile == null)
        {
            SharedFile = new SharedFile(content);
            SharedFileId = SharedFile.Id;
        }
        else
        {
            SharedFile.Content = content;
        }
    }

    public void ReloadFile(SharedFile content)
    {
        SharedFile = content;
    }

    public void AskQuestion(Question question)
    {
        _questions.Add(question);
    }


    // Método para iniciar la sala
    public void StartRoom()
    {
        if (Roomstarted)
        {
            throw new InvalidOperationException("The room is already started.");
        }

        Roomstarted = true;
    }

    // Método para finalizar la sala
    public void EndRoom()
    {
        if (!Roomstarted)
        {
            throw new InvalidOperationException("The room is not active.");
        }

        Roomstarted = false;
    }

    public byte[] GetFileContent()
    {
        return SharedFile?.Content;
    }

    public void AddParticipant(AuthUser user, bool isAnonymous = false)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null.");
        }

        if (Participants.Any(p => p.AuthUserId == user.Id))
        {
            throw new InvalidOperationException("User is already a participant in the room.");
        }

        var participant = new Participant
        {
            Id = Guid.NewGuid(),
            AuthUserId = user.Id,
            IsAnonymous = isAnonymous
        };

        Participants.Add(participant);
    }
    public void RemoveParticipant(Guid participantId)
    {
        // Busca al participante en la lista de participantes
        var participant = Participants.FirstOrDefault(p => p.Id == participantId);

        // Si el participante no existe, lanza una excepción
        if (participant == null)
        {
            throw new InvalidOperationException("Participant not found in the room.");
        }

        // Elimina al participante de la lista
        Participants.Remove(participant);
    }
    
    public void CreatePoll(string question, List<string> options)
    {
        if (string.IsNullOrWhiteSpace(question))
        {
            throw new ArgumentException("Question cannot be empty.", nameof(question));
        }

        if (options == null || options.Count < 2)
        {
            throw new ArgumentException("Poll must have at least two options.", nameof(options));
        }

        if (Polls.Any(p => p.Question == question))
        {
            throw new InvalidOperationException("A poll with the same question already exists in this room.");
        }

        var poll = new Poll(this, Host, question, options);
        Polls.Add(poll);
    }


    // Método para cerrar una encuesta
    public void ClosePoll(Guid pollId)
    {
        var poll = Polls.FirstOrDefault(p => p.Id == pollId);

        if (poll == null)
        {
            throw new InvalidOperationException("Poll not found.");
        }

        poll.ClosePoll();
    }


}