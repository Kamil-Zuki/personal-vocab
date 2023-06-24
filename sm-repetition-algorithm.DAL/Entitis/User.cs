using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sm_repetition_algorithm.DAL.Entitis
{
    public class User
    {
        public User()
        {
            Groups = new HashSet<Group>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
