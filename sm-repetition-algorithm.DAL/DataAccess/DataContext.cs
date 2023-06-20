﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sm_repetition_algorithm.DAL.Entitis;
using Sm_repetition_algorithm.DAL.Entitis;

namespace sm_repetition_algorithm.DAL.DataAccess
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost; Port=5432; Database=glosslore-repetition; User Id=postgres; Password=kamkam14");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add your model configuration here
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<DeckAndTerm> DeckAndTerms { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<RepetitionData> Repetitions { get; set; }
    }
}