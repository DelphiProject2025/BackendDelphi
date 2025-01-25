using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Interfaces.Resources;

namespace delphibackend.Delphi.Interfaces.Transform;

public static class QuestionResourceAssembler
{
    public static QuestionResource ToResource(Question question)
    {
        return new QuestionResource
        {
            Id = question.Id,
            UserId = question.ParticipantId,
            RoomId = question.RoomId,
            Text = question.Text,
            Likes = question.Likes,
            Answer = question.Answer
        };
    }
}
