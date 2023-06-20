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
    public class User
    {
        public User()
        {
            Decks = new HashSet<Deck>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public virtual ICollection<Deck> Decks { get; set; }
    }
}
