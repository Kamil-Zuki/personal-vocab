namespace personal_vocab.DAL.Entitis;

public class Term
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string Transcription { get; set; }
    public string Meaning { get; set; }
    public string Example { get; set; }
    public string Image { get; set; }

    public virtual ICollection<DeckTerms> DeckTerms { get; set; }
    public virtual RepetitionData RepetitionData { get; set; }
}

