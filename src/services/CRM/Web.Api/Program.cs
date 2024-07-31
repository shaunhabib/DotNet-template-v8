
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Extensions;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Integrations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using Web.Framework.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var services= builder.Services;
var connectionString = builder.Configuration.GetConnectionString("PersistenceConnectionForMysqlDb");
// Add services to the container.
//auth added
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

services.AddAuthorization(options =>
{
    options.AddPolicy("ApiReader", policy => policy.RequireClaim("scope", "api.read"));
    options.AddPolicy("SuperAdmin", policy => policy.RequireClaim(ClaimTypes.Role, "SuperAdmin"));
});
services.AddCors();
services.AddAutoMapper();
services.AddFramework(builder.Configuration, connectionString);
services.AddControllers();
services.AddApiVersioningExtension();
services.AddJsonMultipartFormDataSupport(JsonSerializerChoice.Newtonsoft);
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRM Template", Version = "v1" });
    #region auth definations
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityDefinition("clientbusinessprofileId", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the clientbusinessprofileId. 
                      Enter clientbusinessprofileId in the text input below.
                      Example: '19'",
        Name = "clientbusinessprofileId",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "clientbusinessprofileId"
    });
    c.AddSecurityDefinition("userId", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the userId. 
                      Enter userId in the text input below.
                      Example: '19'",
        Name = "userId",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "userId"
    });
    c.AddSecurityDefinition("processcall", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using processcall. 
                      Enter 'true' or 'false' in the text input below.
                      Example: 'true'",
        Name = "processcall",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "processcall"
    });
    #endregion
    c.OperationFilter<SecureEndpointAuthRequirementFilter>();
});


var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;
var appSettingFile = isDevelopment ? $"appsettings.Development.json" : "appsettings.json";
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(appSettingFile, optional: true, reloadOnChange: true)
                .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.Api v1"));
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseApiErrorHandlingMiddleware();
//app.UseApiPermissionMiddleware();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();

#region custom swagger auth
internal class SecureEndpointAuthRequirementFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecurityScheme
                {
                   Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                }] = new List<string>()
            },
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecurityScheme
                {
                   Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "clientbusinessprofileId"
                    },
                    Scheme = "oauth2",
                    Name = "clientbusinessprofileId",
                    In = ParameterLocation.Header,
                }] = new List<string>()
            },
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "userId"
                    },
                    Scheme = "oauth2",
                    Name = "userId",
                    In = ParameterLocation.Header,
                }] = new List<string>()
            },
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecurityScheme
                {
                   Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "processcall"
                    },
                    Scheme = "oauth2",
                    Name = "processcall",
                    In = ParameterLocation.Header,
                }] = new List<string>()
            }
        };
    }
}

#endregion
