using AutoMapper;
using DDD.Domain.Entity;
using DDD.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.API.Application.Commands.Update
{
    public class UpdateProductsCommandHandler : IRequestHandler<UpdateProductsCommand, bool>
    {
        private readonly IRepositoryEF<Products> _repository;
        private readonly IMapper _mapper;
        public UpdateProductsCommandHandler(IRepositoryEF<Products> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductsCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                // throw new ArgumentNullException(nameof(request));
                return false;
            var mode = _repository.GetFirstAsync(request.ProductsCommand.Id).Result;
            if (mode == null)
                return false;
            var result = _mapper.Map<Products>(request.ProductsCommand);
            result.CreateDate = mode.CreateDate;
            result.ModiDate = DateTime.UtcNow;
            _repository.Update(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
