﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace LintCoder.Infrastructure.Persistence
{
    public interface IBaseRepository<T> where T : class
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task AddRangeAsync(IEnumerable<T> t);
        void DeleteRange(IEnumerable<T> t);
        void DeleteRange(Expression<Func<T, bool>> expression);
        void UpdateRange(IEnumerable<T> t);

        IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> QueryByPage<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy);

        T Add(T entity);

        T Update(T entity);

        T Remove(T entity);
        ValueTask<EntityEntry<T>> InsertAsync(T entity);

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task<T> RemoveAsync(T entity);

        Task<bool> IsExistAsync(Expression<Func<T, bool>> whereLambda);

        Task<T> GetEntityAsync(Expression<Func<T, bool>> whereLambda);

        Task<List<T>> SelectAsync();

        Task<List<T>> SelectAsync(Expression<Func<T, bool>> whereLambda);

        Task<Tuple<List<T>, int>> SelectAsync<S>(int pageSize, int pageIndex, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool isAsc);
    }
}
