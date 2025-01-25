using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace delphibackend.Delphi.Infrastructure.Persistence.EFC.Repositories;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{

    public QuestionRepository(AppDbContext context) : base(context) { }



    public async Task<IEnumerable<Question>> GetQuestionsByRoomId(Guid queryRoomId)
    {
        return await Context.Questions
            .Where(q => q.RoomId == queryRoomId)
            .ToListAsync();
    }
    public async Task<IEnumerable<Question>> GetByRoomId(Guid roomId)
    {
        return await Context.Questions.Where(q => q.RoomId == roomId).ToListAsync();
    }
}
