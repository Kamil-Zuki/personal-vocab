using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sm_repetition_algorithm.DAL.Entitis;

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
        public long Id { get; set; }

        public long DeckId { get; set; }
        public long TermId { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Deck> Decs { get; set; }
        public virtual ICollection<Term> Terms { get; set; }
    }
}
