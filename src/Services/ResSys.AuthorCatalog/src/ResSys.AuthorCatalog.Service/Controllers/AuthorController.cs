using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ResSys.AuthorCatalog.Service.Dtos;
using ResSys.AuthorCatalog.Service.Entities;
using ResSys.AuthorCatalog.Service.Extensions;
using ResSys.Common;

namespace ReqSys.AuthorCatalog.Service.Controllers
{
    [ApiController]
    [Route("authors")]
    public class FilmCatalogController : ControllerBase
    {
        private readonly IRepository<Author> authorRepository;


        public FilmCatalogController(IRepository<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAsync()
        {
            var items = (await authorRepository.GetAllAsync())
                .Select(item => item.AsDto());

            return Ok(items);
        }


        [HttpGet("{authorRegNum}")]
        public async Task<ActionResult<Guid>> GetByIdAsync(int authorRegNum)
        {
            var item = (await authorRepository.GetAsync(x => x.AuthorRegNum == authorRegNum));

            return item == null ? NotFound() : item.Id;
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> PostAsync(CreateAuthorDto createAuthorDto)
        {
            var item = new Author
            {
                Name = createAuthorDto.Name,
                AuthorRegNum = createAuthorDto.AuthorRegNum,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await authorRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateAuthorDto updateDTO)
        {
            var item = (await authorRepository.GetAsync(id));

            if (item == null)
                return NotFound();

            item.Name = updateDTO.Name;
            item.AuthorRegNum = updateDTO.AuthorRegNum;

            await authorRepository.UpdateAsync(item);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = (await authorRepository.GetAsync(id));

            if (item == null)
                return NotFound();

            await authorRepository.RemoveAsync(id);

            return NoContent();
        }
    }
}