using DDD.API.Application.Commands.Create;
using DDD.API.Application.Commands.GetAll;
using DDD.Domain.Entity;
using DDD.Domain.IRepositories;
using DDD.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediat;
        public ProductsController(IMediator mediat)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediat.Send(new GetAllProductsCommand() { All = true }));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Products products)
        {

            return Ok(await _mediat.Send(new CreateProductsCommand() { Products = products }));
        }

    }
}
