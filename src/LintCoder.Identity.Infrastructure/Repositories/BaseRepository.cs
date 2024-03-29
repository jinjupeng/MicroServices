﻿using LintCoder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace LintCoder.Identity.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /// <summary>
        /// EF上下文对象
        /// </summary>
        private readonly IdentityDbContext _context;

        public BaseRepository(IdentityDbContext context)
        {
            this._context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> t)
        {
            await _context.AddRangeAsync(t);
        }

        public void DeleteRange(IEnumerable<T> t)
        {
            _context.RemoveRange(t);
        }

        public void DeleteRange(Expression<Func<T, bool>> expression)
        {
            _context.RemoveRange(GetModels(expression));
        }

        public void UpdateRange(IEnumerable<T> t)
        {
            _context.UpdateRange(t);
        }

        public IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda)
        {
            return whereLambda != null ? _context.Set<T>().AsNoTracking().Where(whereLambda) : _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> QueryByPage<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy)
        {
            return _context.Set<T>().Where(whereLambda.Compile()).AsQueryable().OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public async ValueTask<EntityEntry<T>> InsertAsync(T entity)
        {
            return await _context.Set<T>().AddAsync(entity);
        }


        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await _context.Set<T>().AnyAsync(whereLambda);
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(whereLambda);
        }

        public async Task<List<T>> SelectAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> SelectAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await _context.Set<T>().Where(whereLambda).ToListAsync();
        }

        public async Task<Tuple<List<T>, int>> SelectAsync<S>(int pageSize, int pageIndex, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool isAsc)
        {
            var total = await _context.Set<T>().Where(whereLambda).CountAsync();

            if (isAsc)
            {
                var entities = await _context.Set<T>().Where(whereLambda)
                                      .OrderBy<T, S>(orderByLambda)
                                      .Skip(pageSize * (pageIndex - 1))
                                      .Take(pageSize).ToListAsync();

                return new Tuple<List<T>, int>(entities, total);
            }
            else
            {
                var entities = await _context.Set<T>().Where(whereLambda)
                                      .OrderByDescending<T, S>(orderByLambda)
                                      .Skip(pageSize * (pageIndex - 1))
                                      .Take(pageSize).ToListAsync();

                return new Tuple<List<T>, int>(entities, total);
            }
        }

        public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Add(entity));
        }

        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Update(entity));
        }

        public Task<T> RemoveAsync(T entity)
        {
            return Task.FromResult(Remove(entity));
        }

        public T Add(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public T Update(T entity)
        {
            return _context.Set<T>().Update(entity).Entity;
        }

        public T Remove(T entity)
        {
            return _context.Set<T>().Remove(entity).Entity;
        }
    }
}
