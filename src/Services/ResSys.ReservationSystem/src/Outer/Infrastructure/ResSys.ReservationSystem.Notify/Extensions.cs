using Microsoft.Extensions.DependencyInjection;
using ResSys.ReservationSystem.Service.Interfaces;
using MongoDB.Driver;

namespace ResSys.ReservationSystem.Notify
{
    public static class Extensions
    {
        public static IServiceCollection AddReservationNotifier(this IServiceCollection services)
        {
            services.AddScoped<IReservationNotifier, ReservationNotifier>();
            return services;
        }
    }
}