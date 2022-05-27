using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace CurrencyRate.API.Filters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
