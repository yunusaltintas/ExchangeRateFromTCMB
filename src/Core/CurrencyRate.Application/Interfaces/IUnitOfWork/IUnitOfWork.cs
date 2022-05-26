using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Application.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork
    {

        Task CommitAsync();
        void Commit();
    }
}
