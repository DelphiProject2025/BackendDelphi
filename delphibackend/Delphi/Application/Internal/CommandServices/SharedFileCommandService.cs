using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Shared.Domain.Repositories;
using System.Threading.Tasks;
using delphibackend.Delphi.Domain.Services;

namespace delphibackend.Delphi.Application.Internal.CommandServices
{
    public class SharedFileCommandService : ISharedFileCommandService
    {
        private readonly ISharedFileRepository _sharedFileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SharedFileCommandService(ISharedFileRepository sharedFileRepository, IUnitOfWork unitOfWork)
        {
            _sharedFileRepository = sharedFileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SharedFile> Handle(UploadSharedFileCommand command)
        {
            var sharedFile = new SharedFile(command.FileContent);
            await _sharedFileRepository.AddAsync(sharedFile);
            await _unitOfWork.CompleteAsync(); // 🔹 Se usa `IUnitOfWork` para persistencia
            return sharedFile;
        }

        public async Task Handle(RemoveSharedFileCommand command)
        {
            var sharedFile = await _sharedFileRepository.GetSharedFileByIdAsync(command.FileId);
            if (sharedFile != null)
            {
                _sharedFileRepository.Remove(sharedFile);
                await _unitOfWork.CompleteAsync(); // 🔹 Se usa `IUnitOfWork` para persistencia
            }
        }
    }
}