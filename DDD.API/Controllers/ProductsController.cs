using AutoMapper;
using DDD.API.Application.Commands.Create;
using DDD.API.Application.Commands.Delete;
using DDD.API.Application.Commands.Update;
using DDD.API.Application.Models;
using DDD.API.Application.Queries.GetAll;
using DDD.API.Application.Queries.GetFisrt;
using DDD.API.Application.Queries.GetList;
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
        public async Task<IActionResult> Create([FromBody] ProductsCommand products)
        {

            return Ok(await _mediat.Send(new CreateProductsCommand() { ProductsCommand = products }));
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] Products products)
        {

            return Ok(await _mediat.Send(new GetListProductsCommand() {Price = products.Price, Name = products.Name }));
        }

        [HttpPost("first/{id}")]
        public async Task<IActionResult> First(int id)
        {

            return Ok(await _mediat.Send(new GetFirstProductsCommand() { Id = id }));
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody] ProductsCommand products)
        {

            return Ok(await _mediat.Send(new UpdateProductsCommand() { ProductsCommand=products}));
        }


        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            return Ok(await _mediat.Send(new DeleteProductsCommand() { Id=id }));
        }

    }
}
