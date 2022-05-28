using AutoMapper;
using CurrencyRate.Application.Dtos;
using CurrencyRate.Application.Extension;
using CurrencyRate.Application.Interfaces.IRepository;
using CurrencyRate.Application.Interfaces.IService;
using CurrencyRate.Application.Interfaces.IUnitOfWork;
using CurrencyRate.Application.SystemsModels;
using CurrencyRate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CurrencyRate.Infrastructure.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly TcmbSystemModel _tcmbSystemModel;
        private readonly IBaseRepository<ExchangeRate> _baseRepository;
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        private readonly IRestExtension _restExtension;

        public ExchangeService(
            IOptions<TcmbSystemModel> options,
            IMapper mapper,
            IBaseRepository<ExchangeRate> baseRepository,
            IUnitOfWork unitOfWork,
            IRestExtension restExtension)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tcmbSystemModel = options.Value;
            _baseRepository = baseRepository;
            _restExtension = restExtension;
        }

        public async Task GetAndSave()
        {
            var exchangeRates = await GetExchangeRate();

            List<string> filterNames = new List<string>() { "USD", "EUR", "GBP", "CHF", "KWD", "SAR", "RUB" };

            var filters = exchangeRates.Currency.Where(i =>
               i.CurrencyCode == "USD"
            || i.CurrencyCode == "EUR"
            || i.CurrencyCode == "GBP"
            || i.CurrencyCode == "CHF"
            || i.CurrencyCode == "KWD"
            || i.CurrencyCode == "SAR"
            || i.CurrencyCode == "RUB"
            );

            var currencyLit = new List<ExchangeRate>();

            foreach (var item in filters)
            {
                var exchangeRate = new ExchangeRate()
                {
                    CurrencyCode = item.CurrencyCode,
                    Rate = item.ForexBuying,
                    LastUpdated = Convert.ToDateTime(exchangeRates.DataTarihString)
                };
                currencyLit.Add(exchangeRate);
            }

            await AddRangeAsync(currencyLit);
        }

        public async Task<CurrencyListDto?> GetExchangeRate()
        {
            var query = await _restExtension.Get(_tcmbSystemModel.BaseUrl);

            return XmlExtension.Deserilize<CurrencyListDto>(query.Content);
        }

        public async Task<IEnumerable<ExchangeRate>> AddRangeAsync(IEnumerable<ExchangeRate> entities)
        {
            await _baseRepository.AddRangeAsync(entities);

            await _unitOfWork.CommitAsync();

            return entities;
        }

        public async Task AddAsync(ExchangeRate entity)
        {
            await _baseRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<CurrencyDto>> GetAllCurrenciesAsync(bool descending, string propertyName)
        {
            System.Reflection.PropertyInfo prop = typeof(ExchangeRate).GetProperty(propertyName);

            var query = await _baseRepository.GetAllAsync();

            query = descending ? query.OrderByDescending(x => prop.GetValue(x, null)) : query.OrderBy(x => prop.GetValue(x, null));

            return _mapper.Map<List<CurrencyDto>>(query);
        }

        public async Task<List<ChangeByCurrencyDto>> GetByCurrencyAsync(string currency)
        {
            var query = _baseRepository.Query().Where(i => i.CurrencyCode == currency);
            var queryOrder = query.OrderBy(i => i.LastUpdated);
            var queryList = await queryOrder.ToListAsync();

            var model = new List<ChangeByCurrencyDto>();

            for (int i = 0; i < queryList.Count(); i++)
            {
                model.Add(new ChangeByCurrencyDto
                {
                    LastUpdated = queryList[i].LastUpdated,
                    Rate = queryList[i].Rate,
                    CurrencyCode = queryList[i].CurrencyCode,
                    Change = i == 0 ? 0 : (Calculae(i, queryList) ?? 0)
                });
            }
            return model;
        }

        private decimal? Calculae(int i, List<ExchangeRate> model)
        {
            return ((model[i].Rate - model[i - 1].Rate) / model[i].Rate) * 100;
        }
    }
}
