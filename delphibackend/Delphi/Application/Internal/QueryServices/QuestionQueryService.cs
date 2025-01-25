using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.Delphi.Domain.Services;
using delphibackend.Shared.Domain.Repositories;

namespace delphibackend.Delphi.Application.Internal.QueryServices;

public class QuestionQueryService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    : IQuestionQueryService
{


    public async Task<IEnumerable<Question>> Handle(GetQuestionByRoomIdQuery query)
    {
        return await questionRepository.GetByRoomId(query.RoomId);
    }

    public async Task<Question?> Handle(GetQuestionByIdQuery query)
    {
        return await questionRepository.FindByIdAsync(query.QuestionId);
    }
    public async Task<IEnumerable<Question>> Handle(GetQuestionsByRoomQuery query)
    {
        return await questionRepository.GetQuestionsByRoomId(query.RoomId);
    }

}

