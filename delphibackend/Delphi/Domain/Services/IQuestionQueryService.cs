using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Queries;

namespace delphibackend.Delphi.Domain.Services;

public interface IQuestionQueryService
{
    Task<Question?> Handle(GetQuestionByIdQuery query);
    Task<IEnumerable<Question>> Handle(GetQuestionsByRoomQuery query);
    Task<IEnumerable<Question>> Handle(GetQuestionByRoomIdQuery query);
}