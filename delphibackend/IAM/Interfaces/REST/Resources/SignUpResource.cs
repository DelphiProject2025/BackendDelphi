namespace delphibackend.IAM.Interfaces.REST.Resources;

public record SignUpResource(string Email, string Password,string name,string phoneNumber,DateTime dateCreated);