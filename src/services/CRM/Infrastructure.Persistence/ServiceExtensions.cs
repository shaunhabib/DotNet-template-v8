using Infrastructure.Persistence.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared
{
    public static class ServiceExtensions
    {
        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            #region Repositories
            services.AddTransient(typeof(HttpApiHelper), typeof(HttpApiHelper));
            #endregion
        }

    }
}
