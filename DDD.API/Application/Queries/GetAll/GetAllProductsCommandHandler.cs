using DDD.API.Application.Models;
using DDD.Domain.Entity;
using DDD.Domain.IRepositories;
using EventBus.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.API.Application.Queries.GetAll
{
    public class GetAllProductsCommandHandler : IRequestHandler<GetAllProductsCommand, IEnumerable<ProductsDTO>>
    {

        private readonly IDapper _repository;


        public GetAllProductsCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        }
        public async Task<IEnumerable<ProductsDTO>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
        {
            if (request.All)
            {
                string sql = "select * from Products";
                return await _repository.GetAllAync<ProductsDTO>(sql, null, CommandType.Text);
            }
            return null;
        }
    }
}
