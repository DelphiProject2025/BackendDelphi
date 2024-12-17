namespace delphibackend.Delphi.Domain.Model.Entities;

public class SharedFile
{
    public Guid Id { get; private set; }
    public byte[]? Content { get; set; }

    public SharedFile()
    {
        Id = Guid.NewGuid();
    }

    public SharedFile(byte[] content)
    {
        Id = Guid.NewGuid();
        Content = content;
    }
}