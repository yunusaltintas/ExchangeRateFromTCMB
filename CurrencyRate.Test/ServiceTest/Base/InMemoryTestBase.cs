using CurrencyRate.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CurrencyRate.Test.ServiceTest.Base
{
    public class InMemoryTestBase : IDisposable
    {
        protected readonly CurrencyRateDbContext _dbContext;

        public InMemoryTestBase()
        {
            var options = new DbContextOptionsBuilder<CurrencyRateDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _dbContext = new CurrencyRateDbContext(options);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
