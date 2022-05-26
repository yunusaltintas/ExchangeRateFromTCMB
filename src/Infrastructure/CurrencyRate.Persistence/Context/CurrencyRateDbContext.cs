using CurrencyRate.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Persistence.Context
{
    public class CurrencyRateDbContext : DbContext
    {
        public CurrencyRateDbContext(DbContextOptions<CurrencyRateDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}