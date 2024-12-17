namespace delphibackend.IAM.Interfaces.REST.Resources;

public record AuthenticatedUserResource(Guid Id, string Email, string Token);