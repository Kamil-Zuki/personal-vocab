using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personal_vocab.DAL.Entitis
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NewWordAmount { get; set; }
        public int RepeatedWordAmount { get; set;}

        public long UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Deck> Decks { get; set; }
    }
}
