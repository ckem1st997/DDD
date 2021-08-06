using AutoMapper;
using DDD.Domain.Entity;
using DDD.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.API.Application.Commands.Delete
{
    public class DeleteProductsCommandHandler : IRequestHandler<DeleteProductsCommand, bool>
    {
        private readonly IRepositoryEF<Products> _repository;
        private readonly IMapper _mapper;

        public DeleteProductsCommandHandler(IRepositoryEF<Products> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteProductsCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                // throw new ArgumentNullException(nameof(request));
                return false;
            var mode = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            if (mode == null)
                return false;
            _repository.Delete(mode);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}