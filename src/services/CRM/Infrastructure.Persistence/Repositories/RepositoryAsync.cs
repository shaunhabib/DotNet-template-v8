using Core.Domain.Persistence.Contracts;
using Core.Domain.Persistence.Models.Common;
using Core.Domain.Persistence.SharedModels.General;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthenticatedUser _authenticatedUser;

        public RepositoryAsync(AppDbContext appDbContext, IAuthenticatedUser authenticatedUser)
        {
            _dbContext = appDbContext;
            _authenticatedUser = authenticatedUser;
        }
        public virtual IQueryable<T> Entity => _dbContext.Set<T>();

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var rs = await _dbContext.Set<T>().FindAsync(id);
            if (rs != null && rs.ClientBusinessProfileId == _authenticatedUser.ClientBusinessProfileId)
                return rs;
            return null;
        }

        public virtual async Task<int> CountTotalAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(w => w.ClientBusinessProfileId == _authenticatedUser.ClientBusinessProfileId).CountAsync();
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return _dbContext.Set<T>().AsQueryable().Where(w => w.ClientBusinessProfileId == _authenticatedUser.ClientBusinessProfileId);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual async Task<List<T>> AddAsync(List<T> entity)
        {
            await _dbContext.Set<T>().AddRangeAsync(entity);
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            await Task.CompletedTask;
        }

        public virtual async Task UpdateAsync(List<T> entity)
        {
            foreach (var v in entity)
            {
                _dbContext.Entry(v).CurrentValues.SetValues(v);
            }
            await Task.CompletedTask;
        }

        public virtual async Task PermanentDeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public virtual async Task PermanentDeleteAsync(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            if (entity.Predefined == true)
            {
                throw new Exception("Predefined values cannot be deleted!");
            }
            entity.Archived = true;
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            await Task.CompletedTask;
        }
        public virtual async Task DeleteAsync(List<T> entity)
        {
            foreach (var a in entity)
            {
                if (a.Predefined == true)
                {
                    throw new Exception("Predefined values cannot be deleted!");
                }
                a.Archived = true;
                _dbContext.Entry(a).CurrentValues.SetValues(a);
            }
            await Task.CompletedTask;
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(w => w.ClientBusinessProfileId == _authenticatedUser.ClientBusinessProfileId).Where(predicate);
        }

        public IEnumerable<T> AsEnumerable()
        {
            return _dbContext.Set<T>().Where(w => w.ClientBusinessProfileId == _authenticatedUser.ClientBusinessProfileId);
        }

        public IQueryable<T> AsNoTracking()
        {
            return _dbContext.Set<T>().AsNoTracking().Where(w => w.ClientBusinessProfileId == _authenticatedUser.ClientBusinessProfileId);
        }
    }
}
