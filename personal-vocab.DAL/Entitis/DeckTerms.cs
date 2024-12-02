namespace personal_vocab.DAL.Entitis;

public class DeckTerms
{
    public Guid Id { get; set; }

    public Guid DeckId { get; set; }
    public Guid TermId { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public virtual Deck Deck { get; set; }
    public virtual Term Term { get; set; }
}
