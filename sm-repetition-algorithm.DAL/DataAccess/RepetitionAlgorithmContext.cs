using Microsoft.EntityFrameworkCore;
using Sm_repetition_algorithm.DAL.Entitis;

namespace sm_repetition_algorithm.DAL.DataAccess
{
    public class RepetitionAlgorithmContext : DbContext
    {
        public RepetitionAlgorithmContext(DbContextOptions options) : base(options) { }

        public DbSet<Deck> Decks { get; set; } = null!;
    }
}
