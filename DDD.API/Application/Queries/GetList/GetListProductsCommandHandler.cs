using Dapper;
using DDD.API.Application.Models;
using DDD.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.API.Application.Queries.GetList
{
    public class GetListProductsCommandHandler : IRequestHandler<GetListProductsCommand,IEnumerable<ProductsDTO>>
    {

        private readonly IDapper _repository;

        public GetListProductsCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<IEnumerable<ProductsDTO>> Handle(GetListProductsCommand request, CancellationToken cancellationToken)
        {
            string sql = @"select * from Products where Name like @name and Price>= @price";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@name", '%' + request.Name + '%');
            parameter.Add("@price", request.Price);
            return await _repository.GetList<ProductsDTO>(sql, parameter, CommandType.Text);
        }
    }
}