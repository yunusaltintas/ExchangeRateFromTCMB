using AutoMapper;
using CurrencyRate.Application.Extension;
using CurrencyRate.Application.Interfaces.IRepository;
using CurrencyRate.Application.Interfaces.IUnitOfWork;
using CurrencyRate.Application.Mapping;
using CurrencyRate.Application.SystemsModels;
using CurrencyRate.Domain.Entities;
using CurrencyRate.Persistence.Repository;
using CurrencyRate.Persistence.UnitOfWork;
using Microsoft.Extensions.Options;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Test.ServiceTest.Base
{
    public class ServiceTestBase : InMemoryTestBase
    {
        protected IBaseRepository<ExchangeRate> _baseRepository;
        protected IMapper _mapper;
        protected Mock<IRestExtension> _mockRestExtension;
        protected IUnitOfWork _unitOfWork;
        protected IOptions<TcmbSystemModel> _options;

        public ServiceTestBase()
        {
            initRepository();
            initExtension();
            initUnitOfWork();
            initMapper();
            initOptions();
        }

        private void initOptions()
        {
            var model = new TcmbSystemModel()
            {
                BaseUrl = "https://www.tcmb.gov.tr/kurlar/today.xml"
            };
            _options = Options.Create(model);
        }

        private void initUnitOfWork()
        {
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        private void initMapper()
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new GeneralMapping()); });
            _mapper = mappingConfig.CreateMapper();
        }

        private void initRepository()
        {
            _baseRepository = new BaseRepository<ExchangeRate>(_dbContext);
        }

        private void initExtension()
        {
            _mockRestExtension = new Mock<IRestExtension>();
            
        }

        public void ArrangeRestExtension(RestResponse response)
        {
             _mockRestExtension.Setup(r => r.Get(It.IsAny<string>())).ReturnsAsync(response);
        }
    }
}
