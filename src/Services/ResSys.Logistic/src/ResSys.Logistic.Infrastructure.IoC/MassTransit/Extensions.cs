using ResSys.Common;
using Microsoft.Extensions.DependencyInjection;
using ResSys.Common.MassTransit;
using ResSys.Logistic.Application.Interfaces;
using ResSys.Logistic.Infrastructure.Data.MassTransit.Notifier;

namespace ResSys.Logistic.Infrastructure.IoC.MassTransit
{
    public static class Extensions
    {
        // DependencyContainer configuration
        public static IServiceCollection RegisterMassTransit(this IServiceCollection services)
        {
            services.AddMassTransitWithRabbitMQ(typeof(StockSuppliesNotifier));
            services.AddScoped<IStockSuppliesNotifier, StockSuppliesNotifier>();
            return services;
        }
    }
}