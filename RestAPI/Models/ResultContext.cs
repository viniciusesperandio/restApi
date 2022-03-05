using Microsoft.EntityFrameworkCore;

namespace RestAPI.Models
{
    public class ResultContext : DbContext
    {
        public ResultContext(DbContextOptions<ResultContext> options)
            : base(options)
        {
        }

        public DbSet<Result> ResultItems { get; set; } = null!;
    }
}
