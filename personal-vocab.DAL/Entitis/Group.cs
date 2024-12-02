namespace personal_vocab.DAL.Entitis;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int NewWordAmount { get; set; }
    public int RepeatedWordAmount { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<Deck> Decks { get; set; }
}

