﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using personal_vocab.DAL.Entitis;
using NpgsqlTypes;

namespace personal_vocab.DAL.Entitis
{
    public class Deck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public int GroupId { get; set; }

        public virtual DeckTerms? DeckTerm { get; set; }
        public virtual Group Group { get; set; }
    }
}
