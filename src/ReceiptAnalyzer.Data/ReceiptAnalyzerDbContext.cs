using BS.ReceiptAnalyzer.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BS.ReceiptAnalyzer.Data
{
    public class ReceiptAnalyzerDbContext : DbContext
    {
        public DbSet<AnalysisTask> Tasks { get; set; }

        public ReceiptAnalyzerDbContext() : base() { }
        public ReceiptAnalyzerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReceiptAnalyzerDbContext).Assembly);
        }
    }
}
