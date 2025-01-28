using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Interfaces.Resources;

namespace delphibackend.Delphi.Interfaces.Transform;

public class SharedFileWithQuestionsResourceAssembler
{
    public static SharedFileWithQuestionsResource ToResourceFromEntity(
        (byte[]?, IReadOnlyList<Question>?) result)
    {
        return new SharedFileWithQuestionsResource
        {
            SharedFile = result.Item1,
            Questions = result.Item2?.Select(q => new QuestionResource
            {
                Id = q.Id,
                ParticipantId  = q.ParticipantId ,
                RoomId = q.RoomId,
                Text = q.Text,
                Likes = q.Likes
            }).ToList()
        };
    }
}