namespace personal_vocab.DAL.Entitis;

public class RepetitionData
{
    public Guid Id { get; set; }
    public int Repetitions { get; set; } = 0;
    public int Interval { get; set; } = 0;
    public float Easiness { get; set; } = 0.2F;
    public DateTime NextPracticeDate { get; set; } = DateTime.UtcNow;
    public long RepetitionNumber { get; set; } = 0;

    public Guid TermId { get; set; }
    public virtual Term Term { get; set; }
}
