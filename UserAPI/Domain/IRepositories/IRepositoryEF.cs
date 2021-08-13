﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Domain.Entity;
using UserAPI.Domain.SeeWork;

namespace UserAPI.Domain.IRepositories
{
    public interface IRepositoryEF<T> where T : BaseEntity
    {
        public IUnitOfWork UnitOfWork { get; }
        Task<T> GetFirstAsync(int id);
        Task<T> GetFirstAsyncAsNoTracking(int id);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> ListAllAsync();
        Task<int> Update(T entity);
        Task<int> Delete(T entity);

    }
}