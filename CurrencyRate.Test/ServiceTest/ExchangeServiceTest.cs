using CurrencyRate.Application.Dtos.Exceptions;
using CurrencyRate.Infrastructure.Services;
using CurrencyRate.Test.ServiceTest.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyRate.Test.ServiceTest
{
    public class ExchangeServiceTest : ServiceTestBase
    {
        private readonly ExchangeService _exchangeService;

        public ExchangeServiceTest()
        {
            _exchangeService = new ExchangeService(_options, _mapper, _baseRepository, _unitOfWork, _mockRestExtension.Object);
        }

        [Fact]
        public async Task GetAndSave_ThrowException_WhenTCMBReturnsNoContent()
        {
            // arrange
            ArrangeRestExtension(new RestSharp.RestResponse()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Content = String.Empty,
            });

            // act
            var exception = await Assert.ThrowsAsync<CustomException>(async () =>
                await _exchangeService.GetExchangeRateAndSaveJob());

            // assert
            Assert.NotNull(exception);
            Assert.Equal("Opps.", exception.ErrorMessage.ErrorDescription);

        }

        [Fact]
        public async Task GetAndSave_SuccessSaveToDb_WhenTCMBReturnsOk()
        {
            // arrange
            ArrangeRestExtension(new RestSharp.RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = @"<?xml version=""1.0"" encoding=""UTF-8""?><?xml-stylesheet type=""text/xsl"" href=""isokur.xsl""?><Tarih_Date Tarih=""27.05.2022"" Date=""05/27/2022""  Bulten_No=""2022/101"" ><Currency CrossOrder=""0"" Kod=""USD"" CurrencyCode=""USD""><Unit>1</Unit><Isim>ABD DOLARI</Isim><CurrencyName>US DOLLAR</CurrencyName><ForexBuying>16.3479</ForexBuying><ForexSelling>16.3773</ForexSelling><BanknoteBuying>16.3364</BanknoteBuying><BanknoteSelling>16.4019</BanknoteSelling><CrossRateUSD/><CrossRateOther/></Currency><Currency CrossOrder=""1"" Kod=""AUD"" CurrencyCode=""AUD""><Unit>1</Unit><Isim>AVUSTRALYA DOLARI</Isim><CurrencyName>AUSTRALIAN DOLLAR</CurrencyName><ForexBuying>11.6415</ForexBuying><ForexSelling>11.7174</ForexSelling><BanknoteBuying>11.5880</BanknoteBuying><BanknoteSelling>11.7877</BanknoteSelling><CrossRateUSD>1.4010</CrossRateUSD><CrossRateOther/></Currency></Tarih_Date>"
            });

            // act
            await _exchangeService.GetExchangeRateAndSaveJob();

            // assert

            var model = await _baseRepository.GetAllAsync();
            Assert.NotNull(model);
            Assert.Equal(1, model.Count());
        }
    }
}
