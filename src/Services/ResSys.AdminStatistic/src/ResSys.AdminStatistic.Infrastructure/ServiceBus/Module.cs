using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using ResSys.AdminStatistic.Infrastructure.ServiceBus.Consumers;
using ResSys.Common.Settings;

namespace ResSys.AdminStatistic.Infrastructure.ServiceBus.MassTransit
{
    class Module : Autofac.Module
    {
        public string Host { get; set; }
        public string ServiceName { get; set; }

        protected override void Load(ContainerBuilder builder)
        {

            builder.AddMassTransit(x =>
            {
                // Add Consumers
                x.AddConsumers(typeof(ServiceBusConsumerException).Assembly);

                // Bus Configuration
                x.AddBus(context =>
                {
                    var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host(this.Host);

                        cfg.ReceiveEndpoint(this.ServiceName,
                        ep =>
                        {
                            // Configure Endpoint
                            // Configures max number of consumers per specified MSG type
                            // https://masstransit-project.com/understand/under-the-hood.html
                            // relativly high number should be used to ensure msg delivery
                            ep.PrefetchCount = 16;

                            // Configure Consumers
                            ep.ConfigureConsumers(context);
                        });
                    });

                    return bus;
                });
            });
        }
    }
}
