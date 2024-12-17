using delphibackend.Delphi.Domain.Model.Aggregates;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.Delphi.Domain.Model.Entities;

public class SessionRecording
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }

    public Guid HostId { get; set; }
    public Host Host { get; set; }

    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }

    public string RecordingUrl { get; set; }
    public long FileSize { get; set; }
    
    // Constructor principal
    public SessionRecording(Room room, Host host, string recordingUrl, long fileSize)
    {
        if (room == null)
        {
            throw new ArgumentNullException(nameof(room), "Room cannot be null.");
        }

        if (host == null)
        {
            throw new ArgumentNullException(nameof(host), "Host cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(recordingUrl))
        {
            throw new ArgumentException("Recording URL cannot be null or empty.", nameof(recordingUrl));
        }

        if (fileSize < 0)
        {
            throw new ArgumentException("File size cannot be negative.", nameof(fileSize));
        }

        Id = Guid.NewGuid();
        RoomId = room.Id;
        Room = room;
        HostId = host.Id;
        Host = host;
        StartDateTime = DateTime.UtcNow;
        EndDateTime = null; // Inicialmente nula porque la grabación acaba de empezar
        RecordingUrl = recordingUrl;
        FileSize = fileSize;
    }

    // Constructor vacío para ORM
    private SessionRecording() { }

    // Métodos
    public void EndRecording(DateTime endTime, long fileSize)
    {
        if (EndDateTime != null)
        {
            throw new InvalidOperationException("The recording has already been ended.");
        }

        if (endTime <= StartDateTime)
        {
            throw new ArgumentException("End time must be after the start time.", nameof(endTime));
        }

        EndDateTime = endTime;
        FileSize = fileSize;
    }

}