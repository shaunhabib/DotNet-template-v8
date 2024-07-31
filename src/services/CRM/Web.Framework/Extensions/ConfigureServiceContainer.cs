using AutoMapper;
using Core.Application;
using Core.Domain.Persistence.SharedModels.General;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PermissionHelper;
using Web.Framework.Services;

namespace Web.Framework.Extensions
{
    public static class ConfigureServiceContainer
    {
        public static void AddAutoMapper(this IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
                    cfg.AddMaps(new[] {
                        "Web.Framework",
                        "Core.Application",
                        "Infrastructure.Persistence"
                    })
                );
            IMapper mapper = mappingConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }

        public static void AddFramework(this IServiceCollection services, IConfiguration configuration,string connectionString)
        {

            services.AddPersistenceDbContext(configuration, connectionString);
            services.AddTransient<IAuthenticatedUser, AuthenticatedUser>();
            services.AddPersistenceRepositories();
            services.AddApplicationLayer();
            //services.AddRabbitMqInfrastructure();
            services.AddSharedInfrastructure();
            services.AddHttpContextAccessor();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddPermissionService(configuration);

        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {

            #region API Versioning
            // Add API Versioning to the Project
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
            #endregion
        }
    }
}
