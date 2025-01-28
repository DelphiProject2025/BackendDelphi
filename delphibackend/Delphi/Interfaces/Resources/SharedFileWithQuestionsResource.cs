namespace delphibackend.Delphi.Interfaces.Resources;

public class SharedFileWithQuestionsResource
{
    public byte[]? SharedFile { get; set; }
    public List<QuestionResource>? Questions { get; set; }
}