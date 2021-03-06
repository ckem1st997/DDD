using DDD.Domain.Entity;
using DDD.Domain.IRepositories;
using DDD.Domain.SeeWork;
using DDD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure.Repositories
{
    public class RepositoryEF<T> : IRepositoryEF<T> where T : BaseEntity
    {
        private readonly ProductsContext _context;
        private readonly DbSet<T> _dbSet;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public RepositoryEF(ProductsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>() ?? throw new ArgumentNullException(nameof(_context));
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            if (entity.IsTransient())
            {
                return (await _dbSet.AddAsync(entity)).Entity;
            }
            return null;

        }

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }


        public virtual async Task<T> GetFirstAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<T> GetFirstAsyncAsNoTracking(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbSet.Where(x => x.Id > 0).ToListAsync();
        }

        public virtual void Update(T entity)
        {
            //  return _dbSet.Update(entity).Entity;
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
