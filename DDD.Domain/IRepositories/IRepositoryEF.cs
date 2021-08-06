using DDD.Domain.Entity;
using DDD.Domain.SeeWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.IRepositories
{
    public interface IRepositoryEF<T> where T : BaseEntity
    {
        public IUnitOfWork UnitOfWork { get; }
        Task<T> GetFirstAsync(int id);
        Task<T> GetFirstAsyncAsNoTracking(int id);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> ListAllAsync();
        void Update(T entity);
        void Delete(T entity);
 
    }
}