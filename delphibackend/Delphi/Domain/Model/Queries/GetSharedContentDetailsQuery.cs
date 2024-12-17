namespace delphibackend.Delphi.Domain.Model.Queries;

public class GetSharedContentDetailsQuery
{
    public Guid RoomId { get; }

    public GetSharedContentDetailsQuery(Guid roomId)
    {
        RoomId = roomId;
    }
};