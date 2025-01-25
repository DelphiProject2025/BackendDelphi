using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Interfaces.Resources;

namespace delphibackend.Delphi.Interfaces.Transform;

public static class CreateQuestionResourceAssembler
{
    public static CreateQuestionCommand ToCommand(CreateQuestionResource resource)
    {
        return new CreateQuestionCommand(
            resource.ParticipantId,
            resource.RoomId,
            resource.Text
        );
    }
}