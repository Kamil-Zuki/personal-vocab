using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using sm_repetition_algorithm.DAL.Entitis;
using NpgsqlTypes;

namespace sm_repetition_algorithm.DAL.Entitis
{
    public class Deck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }
        public string Name { get; set; }

        public long UserId { get; set; }

        public virtual DeckAndTerm? DeckAndTerm { get; set; }
        public virtual Group Group { get; set; }
    }
}
