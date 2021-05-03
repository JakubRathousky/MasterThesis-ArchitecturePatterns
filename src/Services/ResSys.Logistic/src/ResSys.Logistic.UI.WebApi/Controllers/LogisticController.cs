using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ResSys.Logistic.Application.Interfaces;
using ResSys.Logistic.Application.ViewModels;

namespace ReqSys.Logistic.Service.Controllers
{
    [ApiController]
    [Route("supply")]
    public class LogisticController : ControllerBase
    {
        private readonly IStockSuppliesService service;

        public LogisticController(IStockSuppliesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplyDto>>> GetAsync()
        {
            var items = await service.GetAllAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplyDto>> GetByIdAsync(Guid id)
        {
            var item = await service.GetByIdAsync(id);

            return item == null ? NotFound() : item;
        }

        [HttpPost]
        public async Task<ActionResult<SupplyDto>> PostAsync(CreateSupplyDto createSupplyDto)
        {
            var item = await service.CreateAsync(createSupplyDto);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = (await service.GetByIdAsync(id));

            if (item == null)
                return NotFound();

            await service.DeleteAsync(id);

            return NoContent();
        }
    }
}