using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RestAPI.Models
{
    public class CandidateContext : DbContext
    {
        public CandidateContext(DbContextOptions<CandidateContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var splitStringConverter = new ValueConverter<string[], string>(
                                            v => string.Join(";", v),
                                            v => v.Split(new[] { ';' })
                                );
            modelBuilder.Entity<Candidate>().Property(nameof(Candidate.Technologys)).HasConversion(splitStringConverter);
        }

        public DbSet<Candidate> CandidateItems { get; set; } = null!;
    }
}
