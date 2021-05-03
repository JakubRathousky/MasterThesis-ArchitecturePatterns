using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MassTransit;
using MassTransit.Definition;
using ResSys.Common.Settings;
using System;
using System.Reflection;

namespace ResSys.Common.MassTransit
{
    public static class Extensions
    {
        /// <summary>
        /// Inserts MassTransit service with a RabbitMQ connector
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services, Type type)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumers(type.Assembly);
                x.UsingRabbitMq((context, configurator) =>
                {
                    var configuration = context.GetService<IConfiguration>();
                    var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    configurator.Host(rabbitMQSettings.Host);
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
                });
            });

            services.AddMassTransitHostedService();
            return services;
        }
    }
}