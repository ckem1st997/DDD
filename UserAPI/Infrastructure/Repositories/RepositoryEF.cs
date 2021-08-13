using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Domain.Entity;
using UserAPI.Domain.IRepositories;
using UserAPI.Domain.SeeWork;
using UserAPI.Infrastructure.Context;

namespace UserAPI.Infrastructure.Repositories
{
    public class RepositoryEF<T> : IRepositoryEF<T> where T : BaseEntity
    {
        private readonly UserContext _context;
        private readonly DbSet<T> _dbSet;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public RepositoryEF(UserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>() ?? throw new ArgumentNullException(nameof(_context));
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            return (await _dbSet.AddAsync(entity)).Entity;

        }

        public virtual async Task<int> Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync();
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
            return await _dbSet.AsNoTracking().Where(x => x.Id > 0).ToListAsync();
        }

        public virtual async Task<int> Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
