using CurrencyRate.Domain.Entities;
using CurrencyRate.Domain.EntityTypeBuilder;
using Microsoft.EntityFrameworkCore;

namespace CurrencyRate.Persistence.Context
{
    public class CurrencyRateDbContext : DbContext
    {
        public CurrencyRateDbContext(DbContextOptions<CurrencyRateDbContext> options) : base(options)
        {
        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ExchangeRateTypeBuilder());

            base.OnModelCreating(modelBuilder);
        }
    }
}