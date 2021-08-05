using DDD.Domain.Entity;
using DDD.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.API.Application.Commands.Create
{
    public class CreateProductsCommandHandler : IRequestHandler<CreateProductsCommand, bool>
    {
        private readonly IRepositoryEF<Products> _repository;

        public CreateProductsCommandHandler(IRepositoryEF<Products> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository)); ;
        }

        public async Task<bool> Handle(CreateProductsCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var result = await _repository.AddAsync(request.Products);
            if (result != null)
                return await _repository.UnitOfWork.SaveEntitiesAsync();
            return false;
        }
    }
}
