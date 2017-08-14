namespace Northwind.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entity);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void Delete(params Expression<Func<TEntity, bool>>[] conditions);

        Task DeleteAsync(params Expression<Func<TEntity, bool>>[] conditions);

        TEntity GetById(int id);

        Task<TEntity> GetByIdAsync(int id);

        TEntity GetById(string id);

        Task<TEntity> GetByIdAsync(string id);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
    }
}
