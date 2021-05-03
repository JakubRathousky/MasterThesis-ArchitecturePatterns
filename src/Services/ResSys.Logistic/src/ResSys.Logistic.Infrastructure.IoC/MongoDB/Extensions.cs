using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ResSys.Logistic.Domain.Interfaces;
using ResSys.Logistic.Infrastructure.Data.MongoDB.Repositories;

namespace ResSys.Logistic.Infrastructure.IoC.MongoDB
{
    public static class Extensions
    {
        /// DependencyContainer configuration
        public static IServiceCollection RegisterMongo(this IServiceCollection services, string collectionName)
        {
            services.AddSingleton<IStockSupplyRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new StockSupplyRepository(database, collectionName);
            });
            return services;
        }
    }
}