using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace RestAPI.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public DbSet<Company> CompanyItems { get; set; } = null!;
    }
}
