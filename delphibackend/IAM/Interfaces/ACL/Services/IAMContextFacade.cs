using delphibackend.IAM.Domain.Model.Commands;
using delphibackend.IAM.Domain.Model.Queries;
using delphibackend.IAM.Domain.Services;


namespace delphibackend.IAM.Interfaces.ACL.Services;

public class IamContextFacade(IAuthUserCommandService userCommandService, IAuthUserQueryService userQueryService) : IIamContextFacade
{
    public async Task<Guid> CreateAuthUser(string email, string password,string name,string phonenumber,DateTime dateCreated)
    {
        var signUpCommand = new SignUpCommand(email, password,name,phonenumber,dateCreated);
        await userCommandService.Handle(signUpCommand);
        var getUserByUsernameQuery = new GetAuthUserByEmailQuery(email);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? Guid.Empty;

    }

    public async Task<Guid> FetchAuthUserIdByEmail(string email)
    {
        var getAuthUserByUsernameQuery = new GetAuthUserByEmailQuery(email);
        var result = await userQueryService.Handle(getAuthUserByUsernameQuery);
        return result?.Id ?? Guid.Empty;
    }

    public async Task<string> FetchAuthUsernameByUserId(Guid userId)
    {
        var getAuthUserByIdQuery = new GetAuthUserByIdQuery(userId);
        var result = await userQueryService.Handle(getAuthUserByIdQuery);
        return result?.Email ?? string.Empty;
    }
}