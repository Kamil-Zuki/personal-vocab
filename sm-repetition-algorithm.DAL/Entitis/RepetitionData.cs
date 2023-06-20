using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sm_repetition_algorithm.DAL.Entitis
{
    public class RepetitionData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int Repetitions { get; set; }
        public int Interval { get; set; }
        public float Easiness { get; set; }
        public DateTime NextPracticeDate { get; set; }

        public long TermId { get; set; }
    }
}
