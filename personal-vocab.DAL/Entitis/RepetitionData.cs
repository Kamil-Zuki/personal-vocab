namespace personal_vocab.DAL.Entitis;

public class RepetitionData
{
    public Guid Id { get; set; }
    public int Repetitions { get; set; }
    public int Interval { get; set; }
    public float Easiness { get; set; }
    public DateTime NextPracticeDate { get; set; }
    public long RepetitionNumber { get; set; }

    public Guid TermId { get; set; }
}
