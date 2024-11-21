using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LangVet.Infrastructure
{
    public class LangVetContext : DbContext
    {
        public LangVetContext(DbContextOptions<LangVetContext> options) : base(options)
        {

        }

        public DbSet<HighlightedTerm> highlightedTerms { get; set; }
        public DbSet<Input> inputs { get; set; }
        public DbSet<MarkedDocument> markedDocuments { get; set; }
        public DbSet<OutputTerms>  outputTerms { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=ep-late-scene-a57qwigc.us-east-2.aws.neon.tech;Port=5432;Database=LangVetEntities;Username=neondb_owner;Password=XFdJ35HMSkcP");
        //}
    }
}
