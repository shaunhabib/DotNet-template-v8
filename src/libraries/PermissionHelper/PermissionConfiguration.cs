using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PermissionHelper
{
    public static class PermissionConfiguration
    {
        public static void AddPermissionService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPermissionService), typeof(PermissionService));
        }

        public static void UsePermissionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthorizePermissionMiddleware>();
        }
    }
}
