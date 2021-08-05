using Dapper;
using DDD.Domain.Entity;
using DDD.Domain.IRepositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.Infrastructure.Repositories
{
    public class RepositoryDapper<T> : IRepositoryDapper<T> where T : BaseEntity
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "ProductsContext";
        public RepositoryDapper(IConfiguration config)
        {
            _config = config;
        }

        public async Task<T> GetFirstAsync(int id)
        {
            string sp = "select * from Products where Id=" + id + "";
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await connection.QueryFirstOrDefaultAsync<T>(sp, null, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            string sp = "select * from Products";
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await connection.QueryAsync<T>(sp, null, commandType: CommandType.Text);

        }
    }
}
