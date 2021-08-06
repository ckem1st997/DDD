using AutoMapper;
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
        private readonly IMapper _mapper;
        public CreateProductsCommandHandler(IRepositoryEF<Products> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateProductsCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var mode = _mapper.Map<Products>(request.ProductsCommand);
            mode.CreateDate = DateTime.UtcNow;
            mode.ModiDate = DateTime.UtcNow;
           var t= await _repository.AddAsync(mode);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
