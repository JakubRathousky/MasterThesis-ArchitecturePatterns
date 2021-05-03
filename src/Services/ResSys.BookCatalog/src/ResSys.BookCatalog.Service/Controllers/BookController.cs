using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ResSys.BookCatalog.Service.Dtos;
using ResSys.BookCatalog.Service.Entities;
using ResSys.BookCatalog.Service.Extensions;
using ResSys.Common;
using ResSys.BookCatalog.Contracts;

namespace ReqSys.BookCatalog.Service.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookCatalogController : ControllerBase
    {
        private readonly IRepository<Book> booksRepository;

        private readonly IPublishEndpoint publishEndpoint;

        public BookCatalogController(IRepository<Book> booksRepository, IPublishEndpoint publishEndpoint)
        {
            this.booksRepository = booksRepository;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAsync()
        {
            var items = (await booksRepository.GetAllAsync())
                .Select(item => item.AsDto());

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetByIdAsync(Guid id)
        {
            var item = (await booksRepository.GetAsync(id)).AsDto();

            return item == null ? NotFound() : item;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateBookDto updateDTO)
        {
            var item = (await booksRepository.GetAsync(id));

            if (item == null)
                return NotFound();

            item.Name = updateDTO.Name;
            item.Description = updateDTO.Description;
            item.PublishDate = updateDTO.PublishDate;
            item.NumberOfPages = updateDTO.NumberOfPages;

            await booksRepository.UpdateAsync(item);
            await publishEndpoint.Publish(new BookUpdated(item.Id, item.Name, item.Description, item.NumberOfPages, item.PublishDate));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = (await booksRepository.GetAsync(id));

            if (item == null)
                return NotFound();

            await booksRepository.RemoveAsync(id);

            await publishEndpoint.Publish(new BookDeleted(item.Id));

            return NoContent();
        }
    }
}