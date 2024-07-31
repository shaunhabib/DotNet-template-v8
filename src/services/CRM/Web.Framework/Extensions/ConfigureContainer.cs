using Web.Framework.Middleware;
using Microsoft.AspNetCore.Builder;
using PermissionHelper;

namespace Web.Framework.Extensions
{
    public static class ConfigureContainer
    {
        public static void UseApiErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiErrorHandlerMiddleware>();
        }
        public static void UseApiPermissionMiddleware(this IApplicationBuilder app)
        {
            app.UsePermissionMiddleware();
        }
    }
}
