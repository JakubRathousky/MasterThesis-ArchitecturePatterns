using ResSys.Common;
using Microsoft.Extensions.DependencyInjection;
using ResSys.Common.MassTransit;
using ResSys.Logistic.Application.Interfaces;
using ResSys.Logistic.Infrastructure.Data.MassTransit.Notifier;
using ResSys.Logistic.Application.Services;

namespace ResSys.Logistic.Infrastructure.IoC.Interfaces
{
    public static class extensions
    {
        // DependencyContainer configuration
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IStockSuppliesService, StockSuppliesService>();
            return services;
        }
    }
}