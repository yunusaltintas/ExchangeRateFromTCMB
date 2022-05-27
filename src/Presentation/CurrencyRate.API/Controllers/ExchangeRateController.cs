using CurrencyRate.Application.Dtos;
using CurrencyRate.Application.Dtos.BaseResponse;
using CurrencyRate.Application.Interfaces.IService;
using CurrencyRate.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CurrencyRate.API.Controllers
{
    [Route("api/v1/exchangeRates")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;
        public ExchangeRateController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }


        [HttpGet]
        public async Task<ActionResult<CustomResponseDto<List<CurrencyDto>>>> GetAllCurrencies(bool? descending = true, string? propertyName = "LastUpdated")
        {
            var resultList = await _exchangeService.GetAllCurrenciesAsync(descending.Value, propertyName);

            if (resultList is null)
                return new NoContentResult();

            return new OkObjectResult(
                new CustomResponseDto<List<CurrencyDto>>().Success((int)HttpStatusCode.OK, resultList));
        }

        [HttpGet("{currency}")]
        public async Task<ActionResult<CustomResponseDto<List<ChangeByCurrencyDto>>>> GetByCurrency(string currency)
        {
            var result = await _exchangeService.GetByCurrencyAsync(currency);

            var denemeee = result.ToList();

            if (denemeee is null)
                return new NoContentResult();

            return new OkObjectResult(
                new CustomResponseDto<List<ChangeByCurrencyDto>>().Success((int)HttpStatusCode.OK, denemeee));
        }

    }
}
