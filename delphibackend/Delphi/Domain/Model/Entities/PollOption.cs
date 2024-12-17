namespace delphibackend.Delphi.Domain.Model.Entities;

public class PollOption
{
    public Guid Id { get; private set; } // Identificador único de la opción
    public string OptionText { get; private set; } // Texto de la opción
    public int Votes { get; private set; } // Número de votos que ha recibido la opción

    public PollOption(string optionText)
    {
        if (string.IsNullOrWhiteSpace(optionText))
        {
            throw new ArgumentException("Option text cannot be null or empty.", nameof(optionText));
        }

        Id = Guid.NewGuid();
        OptionText = optionText;
        Votes = 0;
    }
    
    // Constructor vacío para ORM
    private PollOption() { }

    // Método para registrar un voto
    public void Vote()
    {
        Votes++;
    }
    
}