using Xunit;
using NSubstitute;
using System;
using System.Collections.Generic;

using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Application.Commands.Statistics;
using ResSys.AdminStatistic.Domain.Reservation;
using ResSys.AdminStatistic.Application;

using System.Linq;

namespace ResSys.AdminStatistic.Application.Tests
{
    public class GetReservationsPerMonth
    {
        private readonly IReservationReadOnlyRepository readOnlyRepository;

        public GetReservationsPerMonth()
        {
            readOnlyRepository = Substitute.For<IReservationReadOnlyRepository>();
        }

        [Fact]
        public async void Get_amount()
        {
            List<Reservation> reservations = new List<Reservation>(){
                new Reservation() {
                    Id = Guid.NewGuid(),
                    ReservationFrom = DateTime.Parse("2021-04-24"),
                    ReservationTo = DateTime.Parse("2021-05-24"),
                    IsActive = true
                },
                new Reservation() {
                    Id = Guid.NewGuid(),
                    ReservationFrom = DateTime.Parse("2021-04-16"),
                    ReservationTo = DateTime.Parse("2021-05-29"),
                    IsActive = false
                },
                new Reservation() {
                    Id = Guid.NewGuid(),
                    ReservationFrom = DateTime.Parse("2021-01-19"),
                    ReservationTo = DateTime.Parse("2021-05-29"),
                    IsActive = false
                },
                new Reservation() {
                    Id = Guid.NewGuid(),
                    ReservationFrom = DateTime.Parse("2021-11-01"),
                    ReservationTo = DateTime.Parse("2021-12-29"),
                    IsActive = true
                },
                new Reservation(){
                    Id = Guid.NewGuid(),
                    ReservationFrom = DateTime.Parse("2021-11-09"),
                    ReservationTo = DateTime.Parse("2021-12-29"),
                    IsActive = true
                },
            };


            readOnlyRepository
                .GetAllAsync()
                .ReturnsForAnyArgs(reservations);

            var useCase = new GetReservationsPerMonthUseCase(
                readOnlyRepository
            );


            var result = await useCase.Execute();

            foreach (var res in result.Count)
            {
                switch (res.Month)
                {
                    case 1:
                        Assert.Equal(0, res.Number);
                        break;
                    case 2:
                        Assert.Equal(0, res.Number);
                        break;
                    case 3:
                        Assert.Equal(0, res.Number);
                        break;
                    case 4:
                        Assert.Equal(1, res.Number);
                        break;
                    case 5:
                        Assert.Equal(0, res.Number);
                        break;
                    case 6:
                        Assert.Equal(0, res.Number);
                        break;
                    case 7:
                        Assert.Equal(0, res.Number);
                        break;
                    case 8:
                        Assert.Equal(0, res.Number);
                        break;
                    case 9:
                        Assert.Equal(0, res.Number);
                        break;
                    case 10:
                        Assert.Equal(0, res.Number);
                        break;
                    case 11:
                        Assert.Equal(2, res.Number);
                        break;
                    case 12:
                        Assert.Equal(0, res.Number);
                        break;
                    default:
                        Assert.Equal(-1, res.Number);
                        break;
                }
            }
        }
    }
}
