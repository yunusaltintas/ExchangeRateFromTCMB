using CurrencyRate.Application.Interfaces.IUnitOfWork;
using CurrencyRate.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CurrencyRateDbContext _context;

        public UnitOfWork(CurrencyRateDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
