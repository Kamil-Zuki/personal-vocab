namespace personal_vocab.DTOs.Responses;

public class TermDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string Transcription { get; set; }
    public string Meaning { get; set; }
    public string Example { get; set; }
    public string Image { get; set; }

    public Guid DeckId { get; set; }
}
