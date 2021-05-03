using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ResSys.FilmCatalog.Contracts;
using ResSys.FilmCatalog.Service.Dtos;
using ResSys.FilmCatalog.Service.Entities;
using ResSys.FilmCatalog.Service.Extensions;
using ResSys.FilmCatalog.Service.Interfaces;
using ResSys.Common;

namespace ReqSys.FilmCatalog.Service.Controllers
{
    [ApiController]
    [Route("films")]
    public class FilmCatalogController : ControllerBase
    {
        private readonly IFilmService filmService;

        public FilmCatalogController(IFilmService filmService)
        {
            this.filmService = filmService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmDto>>> GetAsync()
        {
            var items = await filmService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilmDto>> GetByIdAsync(Guid id)
        {
            var item = await filmService.FindFilmById(id);

            return item == null ? NotFound() : item;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateFilmDto updateDTO)
        {
            var item = (await filmService.FindFilmById(id));

            if (item == null)
                return NotFound();
            await this.filmService.UpdateFilmAsync(id, updateDTO);

            return NoContent();
        }
    }
}