using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserAPI.Domain.Entity;
using UserAPI.Domain.IRepositories;

namespace UserAPI.Service.UserEntity
{
    public class UserRepositories : IUserRepositories
    {
        private readonly IRepositoryEF<Users> _repositoryEF;


        public UserRepositories(IRepositoryEF<Users> repositoryEF)
        {
            _repositoryEF = repositoryEF;
        }

        public async Task<int> ChangeActiveAsync(int id, bool active)
        {
            if (id < 1)
                throw new NotImplementedException(nameof(id));
            var result = await _repositoryEF.GetFirstAsync(id);
            result.Active = active;
            return await _repositoryEF.Update(result);
        }

        public async Task<int> AddAsync(Users entity)
        {
            if (entity == null)
                throw new NotImplementedException(nameof(entity));
            await _repositoryEF.AddAsync(entity);
            return await _repositoryEF.UnitOfWork.SaveChangesAsync();
        }


        public async Task<int> DeleteAsync(Users entity)
        {
            if (entity == null)
                throw new NotImplementedException(nameof(entity));
            return await _repositoryEF.Delete(entity);

        }

        public async Task<Users> GetFirstAsync(int id)
        {
            if (id < 1)
                throw new NotImplementedException(nameof(id));
            return await _repositoryEF.GetFirstAsync(id);
        }

        public async Task<Users> GetFirstAsyncAsNoTracking(int id)
        {
            if (id < 1)
                throw new NotImplementedException(nameof(id));
            return await _repositoryEF.GetFirstAsyncAsNoTracking(id);
        }

        public async Task<IEnumerable<Users>> ListAllAsync()
        {
            return await _repositoryEF.ListAllAsync();
        }

        public async Task<Paginated> PaginatedList(int index, int number, string key)
        {
            if (index == 0 || number == 0)
                throw new NotImplementedException("ExitsZero");
            Paginated paginated = new Paginated();
            paginated.totalCount = (await _repositoryEF.Expression(x => x.Username.Contains(key) || x.Roleu.Contains(key))).Select(x => x.Id).Count();
            paginated.Users = (await _repositoryEF.Expression(x => x.Username.Contains(key) || x.Roleu.Contains(key))).Skip((index - 1) * number).Take(number);
            return paginated;
        }



        public async Task<int> UpdateAsync(Users entity)
        {
            if (entity == null)
                throw new NotImplementedException(nameof(entity));
            return await _repositoryEF.Update(entity);
        }

        public async Task<IEnumerable<Users>> Expression(Expression<Func<Users, bool>> filter = null)
        {
            if (filter == null)
                throw new NotImplementedException(nameof(filter));
            return await _repositoryEF.Expression(filter);
        }
    }
}
