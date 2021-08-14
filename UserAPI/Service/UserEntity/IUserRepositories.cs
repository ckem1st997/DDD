using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserAPI.Domain.Entity;

namespace UserAPI.Service.UserEntity
{
    public interface IUserRepositories
    {
        Task<Users> GetFirstAsync(int id);
        Task<Users> GetFirstAsyncAsNoTracking(int id);
        Task<int> AddAsync(Users entity);

        Task<IEnumerable<Users>> Expression(Expression<Func<Users, bool>> filter = null);

        Task<IEnumerable<Users>> ListAllAsync();
        Task<Paginated> PaginatedList(int index, int number, string key);
        Task<int> ChangeActiveAsync(int id, bool active);
        Task<int> UpdateAsync(Users entity);
        Task<int> DeleteAsync(Users entity);

    }
}
