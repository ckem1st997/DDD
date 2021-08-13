using AutoMapper;
using DDD.API.Application.Commands.Create;
using DDD.API.Application.Commands.Delete;
using DDD.API.Application.Commands.Update;
using DDD.API.Application.DomainEventHandlers.CreateProducts;
using DDD.API.Application.IntegrationEvents.Events;
using DDD.API.Application.Models;
using DDD.API.Application.Queries.GetAll;
using DDD.API.Application.Queries.GetFisrt;
using DDD.API.Application.Queries.GetList;
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
        public ProductsController(IMediator mediat, IEventBus @event)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _event = @event;
        }

        [HttpPost("getall")]
        public async Task<IActionResult> Get([FromBody] getall getall)
        {
            _event.Publish(getall);
            return Ok(await _mediat.Send(new GetAllProductsCommand() { All = getall.all }));
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
            await _mediat.Publish(new CreateProductDomainEvent(mode));
            // sau đó trả về kết quả
            // note: thêm, sửa, xoá có thể hông cần, tuỳ mục đích
            return CreatedAtRoute("First", new { id = mode.Id }, mode);
        }

        [HttpPost("add-user")]
        public IActionResult CreateUser([FromBody] AddUsersIntegrationEvent addUsers)
        {

            // tạo sự kiện và gửi lên Rabbit
            // nếu server không hoạt động, thì khi hoạt động, sẽ gửi trực tiếp đến server đó
            // nếu server hoạt động, thì sẽ gửi đến luôn để xử lý
            _event.Publish(addUsers);

            return Ok("Xin vui lòng đợi để hoàn tất nha !");
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] Productsr products)
        {
            _event.Publish(products);
            return Ok(await _mediat.Send(new GetListProductsCommand() { Price = products.Price, Name = products.Name }));
        }

        public record getall : IntegrationEvent
        {
            public bool all { get; set; }
        }
        public record Productsr : IntegrationEvent
        {
            // public int Id { get; set; }
            public string Name { get; set; }

            public decimal Price { get; set; }

            public string Image { get; set; }
            public DateTime? CreateDate { get; set; }
            public DateTime? ModiDate { get; set; }
        }
        [HttpPost("first", Name = "First")]
        public async Task<IActionResult> First(int id)
        {
            return Ok(await _mediat.Send(new GetFirstProductsCommand() { Id = id }));
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
