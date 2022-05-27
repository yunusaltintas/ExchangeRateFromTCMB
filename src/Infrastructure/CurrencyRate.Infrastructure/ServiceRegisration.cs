using CurrencyRate.Application.Interfaces.IService;
using CurrencyRate.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure
{
    public static class ServiceRegisration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {

            //services.AddScoped<IServiceManager, ServiceManager>();
            //services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            //services.AddScoped<IPastTranslationService, PastTranslationService>();
            services.AddScoped<IExchangeService, ExchangeService>();
        }

    }
}
