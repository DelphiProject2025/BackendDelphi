using delphibackend.IAM.Domain.Model.Commands;
using delphibackend.IAM.Interfaces.REST.Resources;

namespace delphibackend.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}