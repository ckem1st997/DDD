using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> AddAsync(Users entity)
        {
            if (entity == null)
                throw new NotImplementedException(nameof(entity));
            await _repositoryEF.AddAsync(entity);
            return await _repositoryEF.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> Delete(Users entity)
        {
            if (entity == null)
                throw new NotImplementedException(nameof(entity));
            return await _repositoryEF.Delete(entity);

        }

        public async Task<Users> GetFirstAsync(int id)
        {
            if (id <= 1)
                throw new NotImplementedException(nameof(id));
            return await _repositoryEF.GetFirstAsync(id);
        }

        public async Task<Users> GetFirstAsyncAsNoTracking(int id)
        {
            if (id <= 1)
                throw new NotImplementedException(nameof(id));
            return await _repositoryEF.GetFirstAsyncAsNoTracking(id);
        }

        public async Task<IEnumerable<Users>> ListAllAsync()
        {
            return await _repositoryEF.ListAllAsync();
        }

        public async Task<int> Update(Users entity)
        {
            if (entity == null)
                throw new NotImplementedException(nameof(entity));
            return await _repositoryEF.Update(entity);
        }

    }
}
