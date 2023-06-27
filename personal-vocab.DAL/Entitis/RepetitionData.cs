using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personal_vocab.DAL.Entitis
{
    public class RepetitionData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Repetitions { get; set; }
        public int Interval { get; set; }
        public float Easiness { get; set; }
        public DateTime NextPracticeDate { get; set; }
        public long RepetitionNumber { get; set; }

        public int TermId { get; set; }
    }
}
