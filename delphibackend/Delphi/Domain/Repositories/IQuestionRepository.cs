using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Shared.Domain.Repositories;

namespace delphibackend.Delphi.Domain.Repositories
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        Task<IEnumerable<Question>> GetByRoomId(Guid roomId);
        Task<IEnumerable<Question>> GetQuestionsByRoomId(Guid queryRoomId);
        
    }
}