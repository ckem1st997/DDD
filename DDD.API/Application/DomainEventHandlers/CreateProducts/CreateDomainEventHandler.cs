using DDD.Domain.Entity;
using DDD.Domain.Events;
using DDD.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.API.Application.DomainEventHandlers.CreateProducts
{

    // giả sử check đơn hàng xong sẽ tiến hành gửi email, thì cái này dùng để gửi email mà hông cần chạy code trong controller
    // tách việc gửi email ra
    // b1: tạo đơn hàng
    // b2: gọi cái này và truyền vào biến để nó check
    // b3: thoả mãn thì sẽ viết code làm gì đấy, ở đây có thể là gửi email, thay vì phải gọi ngoài controller
    public class CreateDomainEventHandler : INotificationHandler<CreateProductDomainEvent>
    {
        private readonly IRepositoryEF<Products> _repository;

        public CreateDomainEventHandler(
            IRepositoryEF<Products> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateProductDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            // MessageResponse message = new MessageResponse();
            if (domainEvent == null)
            {
                throw new ArgumentNullException(nameof(domainEvent));
            }
            if (domainEvent.Products != null)
            {
                domainEvent.Products.Image = "test INotificationHandler thành công";
                _repository.Update(domainEvent.Products);
                await _repository.UnitOfWork.SaveEntitiesAsync();
            }
        }
    }
}

