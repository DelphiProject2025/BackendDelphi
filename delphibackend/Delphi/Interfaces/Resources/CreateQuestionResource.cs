namespace delphibackend.Delphi.Interfaces.Resources;

public class CreateQuestionResource
{
    public Guid ParticipantId { get; set; }
    public Guid RoomId { get; set; }
    public string Text { get; set; }
}