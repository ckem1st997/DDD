using DDD.Domain.Entity;
using DDD.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.API.Application.Commands.GetAll
{
    public class GetAllProductsCommandHandler : IRequestHandler<GetAllProductsCommand, IEnumerable<Products>>
    {

        private readonly IRepositoryDapper<Products> _repository;

        public GetAllProductsCommandHandler(IRepositoryDapper<Products> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<IEnumerable<Products>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
        {
            if (request.All)
            {
                return await _repository.ListAllAsync();
            }
            return null;
        }
    }
}
