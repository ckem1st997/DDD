using Dapper;
using DDD.API.Application.Models;
using DDD.Domain.Entity;
using DDD.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.API.Application.Queries.GetFisrt
{
    public class GetFirstProductsCommandHandler : IRequestHandler<GetFirstProductsCommand, ProductsDTO>
    {

        private readonly IDapper _repository;

        public GetFirstProductsCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<ProductsDTO> Handle(GetFirstProductsCommand request, CancellationToken cancellationToken)
        {
            string sql = @"select TOP 1 * from Products where Id=@id";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id", request.Id);
            return await _repository.GetAyncFirst<ProductsDTO>(sql, parameter, CommandType.Text);
        }
    }
}
