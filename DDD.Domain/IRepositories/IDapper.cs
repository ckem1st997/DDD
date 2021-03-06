using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.IRepositories
{
    public interface IDapper : IDisposable
    {
        DbConnection GetDbconnection();
        Task<T> GetAyncFirst<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> GetList<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> GetAllAync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

    }
}