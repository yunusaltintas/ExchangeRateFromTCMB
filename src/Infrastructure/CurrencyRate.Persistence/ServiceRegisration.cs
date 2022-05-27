using CurrencyRate.Application.Interfaces.IRepository;
using CurrencyRate.Application.Interfaces.IUnitOfWork;
using CurrencyRate.Persistence.Context;
using CurrencyRate.Persistence.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Persistence
{
    public static class ServiceRegisration
    {
        public static void AddPersistanceServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CurrencyRateDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, CurrencyRate.Persistence.UnitOfWork.UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

        public static void DatabaseInitialize(this IApplicationBuilder builder)
        {
            using var serviceScope =
                builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<CurrencyRateDbContext>();

            if (context == null) return;
                DatabaseMigration(context);

            context.SaveChanges();
        }

        private static void DatabaseMigration(CurrencyRateDbContext context)
        {
            context.Database.Migrate();
        }


    }
}
