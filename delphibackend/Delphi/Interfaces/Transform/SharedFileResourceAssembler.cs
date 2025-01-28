using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Interfaces.Resources;

namespace delphibackend.Delphi.Interfaces.Transform;

public class SharedFileResourceAssembler
{
    public static SharedFileResource ToResourceFromEntity(SharedFile sharedFile)
    {
        return new SharedFileResource
        {
            Id = sharedFile.Id,
            Content = sharedFile.Content
        };
    }
}