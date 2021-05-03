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
    [Route("synchronize")]
    public class ProcessController : ControllerBase
    {
        private readonly IStockSuppliesService service;

        public ProcessController(IStockSuppliesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> SynchronizeAsync()
        {
            await service.Synchronize();

            return Ok();
        }
    }
}