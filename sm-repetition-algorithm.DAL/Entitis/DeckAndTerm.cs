using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sm_repetition_algorithm.DAL.Entitis
{
    public class DeckAndTerm
    {
        public DeckAndTerm() 
        {
            Decs =  new HashSet<Deck>();
            Terms = new HashSet<Term>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public short DeckId { get; set; }
        public short TermId { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Deck> Decs { get; set; }
        public virtual ICollection<Term> Terms { get; set; }
    }
}
