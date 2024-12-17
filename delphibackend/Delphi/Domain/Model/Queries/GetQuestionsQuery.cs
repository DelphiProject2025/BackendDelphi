namespace delphibackend.Delphi.Domain.Model.Queries;

public class GetQuestionsQuery
{
    public Guid RoomId { get; }

    public GetQuestionsQuery(Guid roomId)
    {
        RoomId = roomId;
    }
}