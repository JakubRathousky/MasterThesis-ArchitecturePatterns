using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.ReservationSystem.Service.Dtos;

namespace ResSys.ReservationSystem.Service.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetReservationsAsync();
        Task<ReservationDto> GetReservationByIdAsync(Guid id);
        Task<ReservationDto> SaveReservationAsync(CreateReservationDto createReservationDto);
        Task DeleteAsync(Guid id);
        Task<int> GetAmountOfReservedBookAsync(Guid id, DateTimeOffset date);
        Task<int> GetAmountOfReservedFilmAsync(Guid id, DateTimeOffset date);
    }
}