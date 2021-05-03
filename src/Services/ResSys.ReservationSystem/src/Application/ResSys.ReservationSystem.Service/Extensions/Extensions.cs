using Microsoft.Extensions.DependencyInjection;
using ResSys.ReservationSystem.Service.Interfaces;
using ResSys.ReservationSystem.Service;
using MongoDB.Driver;

namespace ResSys.ReservationSystem.Service.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddServiceReservationDI(this IServiceCollection services)
        {
            services.AddScoped<IReservationService, ReservationService>();
            return services;
        }
    }
}