using Microsoft.Extensions.DependencyInjection;
using ResSys.ReservationSystem.Service.Interfaces;
using MongoDB.Driver;

namespace ResSys.ReservationSystem.Repo
{
    public static class Extensions
    {
        public static IServiceCollection AddReservationMongoRepository(this IServiceCollection services, string collectionName)
        {
            services.AddSingleton<IReservationRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new ReservationRepository(database, collectionName);
            });
            return services;
        }
    }
}