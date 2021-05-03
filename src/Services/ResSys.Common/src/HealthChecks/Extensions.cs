using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MassTransit;
using MassTransit.Definition;
using ResSys.Common.Settings;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace ResSys.Common.HealthChecksService
{
    public static class Extensions
    {
        /// <summary>
        /// Adds a new healthcheck service
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddHealthCheck(this IServiceCollection services)
        {

            var serviceProvider = services.BuildServiceProvider();

            var configuration = serviceProvider.GetService<IConfiguration>();
            var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            services.AddHealthChecks()
                .AddCheck(serviceSettings.ServiceName, () => HealthCheckResult.Healthy("Service is running OK"), new[] { "service" });

            return services;
        }

        /// <summary>
        /// Registers a new healthcheck endpoint
        /// </summary>
        /// <returns></returns>
        public static IEndpointRouteBuilder AddHealthCheckEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/quickHealth", new HealthCheckOptions()
            {
                Predicate = _ => false
            });
            endpoints.MapHealthChecks("/health/services", new HealthCheckOptions()
            {
                Predicate = reg => reg.Tags.Contains("service"),
                ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
            });
            return endpoints;
        }
    }
}