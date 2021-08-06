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
            string sql = @"select * from Products where Id=@id and Name like @name and Price>= @price";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id", request.Id);
            parameter.Add("@name", '%' + request.Name + '%');
            parameter.Add("@price", request.Price);
            return await _repository.GetAyncFirst<ProductsDTO>(sql, parameter, CommandType.Text);
        }
    }
}
