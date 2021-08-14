using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Domain.Entity;

namespace UserAPI.Service.UserEntity
{
    public interface IUserRepositories
    {
        Task<Users> GetFirstAsync(int id);
        Task<Users> GetFirstAsyncAsNoTracking(int id);
        Task<int> AddAsync(Users entity);
        Task<IEnumerable<Users>> ListAllAsync();
        Task<IEnumerable<Users>> PaginatedList();
        Task<int> Update(Users entity);
        Task<int> Delete(Users entity);

    }
}
