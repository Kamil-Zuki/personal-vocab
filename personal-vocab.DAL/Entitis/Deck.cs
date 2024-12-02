namespace personal_vocab.DAL.Entitis;

public class Deck
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Guid GroupId { get; set; }

    public virtual Group Group { get; set; }
    public virtual ICollection<DeckTerms> DeckTerms { get; set; }
}
