using AutoMapper;
using DDD.API.Application.Commands.Create;
using DDD.API.Application.Commands.Delete;
using DDD.API.Application.Commands.Update;
using DDD.API.Application.IntegrationEvents;
using DDD.API.Application.IntegrationEvents.Events;
using DDD.API.Application.Models;
using DDD.API.Application.Queries.GetAll;
using DDD.API.Application.Queries.GetFisrt;
using DDD.API.Application.Queries.Paginated;
using DDD.Domain.Entity;
using DDD.Domain.Events;
using DDD.Domain.IRepositories;
using DDD.Infrastructure.Context;
using EventBus.Abstractions;
using EventBus.Events;
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
        private readonly IEventBus _event;
        private readonly IProductsIntegrationEventService _productsIntegrationEventService;
        public ProductsController(IMediator mediat, IEventBus @event, IProductsIntegrationEventService productsIntegrationEventService)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _event = @event;
            _productsIntegrationEventService = productsIntegrationEventService;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> Get()
        {
            // _event.Publish(getall);
            return Ok(await _mediat.Send(new GetAllProductsCommand() { All = true, BypassCache = true }));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductsCommand products)
        {
            // tiến hành tạo thực thể mới
            // ví dụ đây là tạo đơn hàng mới
            var mode = await _mediat.Send(new CreateProductsCommand() { ProductsCommand = products });
            // truyền thực thể vào INotificationHandler để xử lý tác vụ tiếp theo
            // ví dụ sau khi tạo đơn hàng thành công, sẽ gửi email, và phần check đơn hàng
            // rồi gửi email sẽ được thực hiện tạo phương thức sau, ở đây là : CreateDomainEventHandler
            //  await _mediat.Publish(new CreateProductDomainEvent(mode));
            // sau đó trả về kết quả
            // note: thêm, sửa, xoá có thể hông cần, tuỳ mục đích
            // return CreatedAtRoute("First", new { id = mode.Id }, mode);
            return Ok(mode);
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> CreateUserAsync([FromBody] AddUsersIntegrationEvent addUsers)
        {

            // tạo sự kiện và gửi lên Rabbit
            // nếu server không hoạt động, thì khi hoạt động, sẽ gửi trực tiếp đến server đó
            // nếu server hoạt động, thì sẽ gửi đến luôn để xử lý

            //  _event.Publish(addUsers);


            // lưu lại dữ liệu định public vào db
            await _productsIntegrationEventService.SaveEventAndCatalogContextChangesAsync(addUsers);
            // tiến hành public
            await _productsIntegrationEventService.PublishThroughEventBusAsync(addUsers);

            return Ok("Xin vui lòng đợi để hoàn tất nha !");
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] PaginatedListCommand paginatedList)
        {
            return Ok(await _mediat.Send(new PaginatedListCommand() { KeySearch = paginatedList.KeySearch, PageNumber = paginatedList.PageNumber, fromPrice = paginatedList.fromPrice, toPrice = paginatedList.toPrice, PageIndex = paginatedList.PageIndex }));
        }

        [HttpPost("first", Name = "First")]
        public async Task<IActionResult> First(int id)
        {
            return Ok(await _mediat.Send(new GetFirstProductsCommand() { Id = id, BypassCache = true }));
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody] ProductsCommand products)
        {

            return Ok(await _mediat.Send(new UpdateProductsCommand() { ProductsCommand = products }));
        }


        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            return Ok(await _mediat.Send(new DeleteProductsCommand() { Id = id }));
        }

    }
}
