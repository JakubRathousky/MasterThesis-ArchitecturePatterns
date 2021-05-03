using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using ResSys.Common;
using ResSys.Common.MongoDB;
using ResSys.ReservationSystem.Service.Interfaces;
using ResSys.ReservationSystem.Service.Dtos;
using ResSys.ReservationSystem.Contracts;
using ResSys.ReservationSystem.Contracts.Dtos;

namespace ResSys.ReservationSystem.Notify
{
    public class ReservationNotifier : IReservationNotifier
    {

        private readonly IPublishEndpoint publishEndpoint;
        public ReservationNotifier(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task ReservationCreatedNotification(ReservationDto reservation)
        {
            await publishEndpoint.Publish(
                new ReservationCreated(
                        reservation.Id,
                        reservation.Books.Select(x => new Contracts.Dtos.ReservationItem(x.Id, x.BookId, x.FilmId, x.Amount)),
                        reservation.Films.Select(x => new Contracts.Dtos.ReservationItem(x.Id, x.BookId, x.FilmId, x.Amount)),
                        reservation.ReservationFrom,
                        reservation.ReservationTo
            ));
        }
        public async Task ReservationDeactivateNotification(Guid id)
        {
            await publishEndpoint.Publish(
                new ReservationDeleted(id)
            );
        }
    }
}
