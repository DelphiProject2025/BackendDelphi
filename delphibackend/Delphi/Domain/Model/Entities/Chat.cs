namespace delphibackend.Delphi.Domain.Model.Entities;

public class Chat
{
    public Guid Id { get; private set; }
    public bool isActivated { get; set; }

    public Chat()
    {
        Id = Guid.NewGuid();
    }
}