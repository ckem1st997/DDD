using DDD.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.Domain.IRepositories
{
    public interface IRepositoryDapper<T> where T : BaseEntity
    {
      //  Task DispatchDomainEventsAsync(CancellationToken cancellationToken);
        Task<T> GetFirstAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
    }
}