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

namespace DDD.API.Application.Queries.Paginated
{
    public class PaginatedListCommandHandler : IRequestHandler<PaginatedListCommand, PaginatedList>
    {

        private readonly IDapper _repository;


        public PaginatedListCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        }
        public async Task<PaginatedList> Handle(PaginatedListCommand request, CancellationToken cancellationToken)
        {
            PaginatedList list = new PaginatedList();
            if (request == null)
                return list;
            request.KeySearch = request.KeySearch?.Trim();
            if (request.KeySearch == null)
                request.KeySearch = "";
            string sql = @"select * from Products where Name like @key and Price>= @fromPrice and Price<= @toPrice order by Name OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.KeySearch + '%');
            parameter.Add("@fromPrice", request.fromPrice);
            parameter.Add("@toPrice", request.toPrice == 0 ? 100000 : request.toPrice);
            parameter.Add("@skip", (request.PageIndex - 1) * request.PageNumber);
            parameter.Add("@take", request.PageNumber);
            list.ProductsDTOs = await _repository.GetList<ProductsDTO>(sql, parameter, CommandType.Text);
            string count = @"select count(Id) from Products where Name like @key and Price>= @fromPrice and Price<= @toPrice";
            list.totalCount = await _repository.GetAyncFirst<int>(count, parameter, CommandType.Text);
            return list;
        }
    }
}
