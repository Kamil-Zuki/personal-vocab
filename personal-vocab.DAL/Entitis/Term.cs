using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personal_vocab.DAL.Entitis
{
    public class Term
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Transcription { get; set; }
        public string Meaning { get; set; }
        public string Example { get; set; }
        public string Image { get; set; }

        public virtual DeckTerms DeckTerm { get; set; }
    }
}
