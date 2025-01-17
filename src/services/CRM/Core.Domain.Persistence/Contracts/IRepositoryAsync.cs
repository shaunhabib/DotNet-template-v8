﻿using Core.Domain.Persistence.Models.Common;
using System.Linq.Expressions;

namespace Core.Domain.Persistence.Contracts
{
    public interface IRepositoryAsync<T> where T : BaseEntity
    {
        IQueryable<T> Entity { get; }

        Task<T> GetByIdAsync(int id);

        Task<int> CountTotalAsync();

        Task<T> AddAsync(T entity);

        Task<List<T>> AddAsync(List<T> entity);

        Task UpdateAsync(T entity);
        Task UpdateAsync(List<T> entity);
        Task PermanentDeleteAsync(T entity);
        Task PermanentDeleteAsync(List<T> entities);

        Task DeleteAsync(T entity);
        Task DeleteAsync(List<T> entity);

        IQueryable<T> AsQueryable();
        IQueryable<T> AsNoTracking();
        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);
        IEnumerable<T> AsEnumerable();
    }
}
