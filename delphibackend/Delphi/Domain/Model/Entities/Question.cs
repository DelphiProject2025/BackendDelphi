﻿using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.User.Domain.Model.Entities;

namespace delphibackend.Delphi.Domain.Model.Entities;

public class Question
{
    public Guid Id { get; private set; }
    public Guid ParticipantId { get; private set; }
    public Participant participant { get; private set; }
    
    public Guid RoomId { get; private set; }
    public Room room { get; private set; }
    
    public string Text { get; private set; }
    public string? Answer { get; private set; }
    
    public int Likes { get; private set; }

    public Question(Guid participantId, Guid roomId, string text)
    {
        if (participant == null)
        {
            throw new ArgumentNullException(nameof(participant), "Participant cannot be null.");
        }

        if (room == null)
        {
            throw new ArgumentNullException(nameof(room), "Room cannot be null.");
        }
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Question text cannot be null or empty.", nameof(text));
        }
        
        Id = Guid.NewGuid();
        ParticipantId = participantId;
        RoomId = roomId;
        Text = text;
        Likes = 0;
        Answer = null;
    }
    
    private Question(){}
    
    public void Like()
    {
        Likes++;
    }
}