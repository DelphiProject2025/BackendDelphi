using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Model.Entities;

namespace delphibackend.Delphi.Domain.Services;

public interface ISharedFileCommandService
{
    Task<SharedFile> Handle(UploadSharedFileCommand command);
    Task Handle(RemoveSharedFileCommand command);
}