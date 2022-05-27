using CurrencyRate.Application.Dtos;
using CurrencyRate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Application.Interfaces.IService
{
    public interface IExchangeService
    {
        Task<CurrencyListDto?> GetExchangeRate();
        Task AddAsync(ExchangeRate entity);
        Task<IEnumerable<ExchangeRate>> AddRangeAsync(IEnumerable<ExchangeRate> entities);
        Task GetAndSave();
        Task<List<CurrencyDto>> GetAllCurrenciesAsync(bool descending, string propertyName);
        Task<List<ChangeByCurrencyDto>> GetByCurrencyAsync(string currency);
    }
}
