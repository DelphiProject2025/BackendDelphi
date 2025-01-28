using delphibackend.IAM.Domain.Model.Commands;
using delphibackend.IAM.Interfaces.REST.Resources;

namespace delphibackend.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Email, resource.Password,resource.name,resource.phoneNumber,DateTime.UtcNow);
    }
}