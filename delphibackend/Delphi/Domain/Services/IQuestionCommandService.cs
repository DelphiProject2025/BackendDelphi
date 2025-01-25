using delphibackend.Delphi.Domain.Model.Commands;

namespace delphibackend.Delphi.Domain.Services;

public interface IQuestionCommandService
{
    Task<Guid> Handle(CreateQuestionCommand command);
    Task Handle(LikeQuestionCommand command);
    Task Handle(AnswerQuestionCommand command); 

}