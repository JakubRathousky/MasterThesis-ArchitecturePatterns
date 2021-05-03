using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ResSys.ReservationSystem.Data.Entities;
using ResSys.ReservationSystem.Repo;
using ResSys.ReservationSystem.Service.Extensions;
using ResSys.ReservationSystem.Service.Dtos;
using ResSys.ReservationSystem.Contracts;
using ResSys.ReservationSystem.Service;
using ResSys.ReservationSystem.Service.Interfaces;

namespace ResSys.ReservationSystem.WebApi.Controllers
{
    [ApiController]
    [Route("reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;

        private readonly IPublishEndpoint publishEndpoint;

        public ReservationController(IReservationService reservationService, IPublishEndpoint publishEndpoint)
        {
            this.reservationService = reservationService;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAsync()
        {
            var items = await reservationService.GetReservationsAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetByIdAsync(Guid id)
        {
            var item = await reservationService.GetReservationByIdAsync(id);

            return item == null ? NotFound() : item;
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDto>> PostAsync(CreateReservationDto createReservationDto)
        {
            var item = await reservationService.SaveReservationAsync(createReservationDto);

            return CreatedAtAction(nameof(GetByIdAsync), new
            {
                id = item.Id
            }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await reservationService.GetReservationByIdAsync(id);

            if (item == null)
                return NotFound();

            await reservationService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("book/{id}/{date}")]
        public async Task<int> GetAmountOfReservedBook(Guid id, DateTimeOffset date)
        {
            return (await reservationService.GetAmountOfReservedBookAsync(id, date));
        }

        [HttpGet("film/{id}/{date}")]
        public async Task<int> GetAmountOfReservedFilm(Guid id, DateTimeOffset date)
        {
            return (await reservationService.GetAmountOfReservedFilmAsync(id, date));
        }
    }
}