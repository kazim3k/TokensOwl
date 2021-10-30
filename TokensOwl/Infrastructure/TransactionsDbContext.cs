using Microsoft.EntityFrameworkCore;
using TokensOwl.Infrastructure.Models;

namespace TokensOwl.Infrastructure
{
    public class TransactionsDbContext : DbContext
    {
        public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options)
            : base(options)
        {
        }
        public DbSet<CashedTransaction> CashedTransactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CashedTransaction>()
                .HasKey(i => i.Address);
            
            modelBuilder.Entity<CashedTransaction>()
                .HasIndex(v => new { v.Hash, v.Address })
                .IsCreatedConcurrently();
            
            modelBuilder.Entity<CashedTransaction>()
                .HasIndex(v => new { v.Address })
                .IsUnique()
                .IsCreatedConcurrently();
        }
    }
}