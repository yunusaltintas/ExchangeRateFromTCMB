using CurrencyRate.API.Filters;
using CurrencyRate.API.Middlewares;
using CurrencyRate.Application;
using CurrencyRate.Application.Interfaces.IService;
using CurrencyRate.Application.SystemsModels;
using CurrencyRate.Infrastructure;
using CurrencyRate.Persistence;
using Hangfire;
using Hangfire.SqlServer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.Configure<TcmbSystemModel>(builder.Configuration.GetSection("TcmbSystemModel"));

builder.Services.AddApplicationRegistration();
builder.Services.AddPersistanceServices(connectionString);
builder.Services.AddInfrastructureServices();

builder.Services.AddHangfire(configuration => { configuration.UseSqlServerStorage(connectionString); });
builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.DatabaseInitialize();

app.UseHangfireServer();
app.UseHangfireDashboard(
    "/hangfire",
    new DashboardOptions
    {
        Authorization = new[] { new HangfireAuthorizationFilter() },
    });
var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();
recurringJobManager.AddOrUpdate<IExchangeService>("ExchangeSaveJob", o => o.GetAndSave(), "35 12 * * *");

app.UseMiddleware<RequestResponseMiddleware>();
app.UseRouting();
app.UseEndpoints(endpoint => { endpoint.MapControllers(); });
app.Run();
