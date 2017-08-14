namespace Northwind.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        protected NorthwindDbContext context;
        protected DbSet<TEntity> dbSet;

        protected RepositoryBase(NorthwindDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                dbSet.Add(entity);
            }
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await Task.Run(() => dbSet.Add(entity));
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entity)
        {
            await Task.Run(() => dbSet.AddRange(entity));
        }

        public virtual void Update(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = context.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = context.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                await Task.Run(() => dbSet.Attach(entity));
            }
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => dbSet.Remove(entity));
        }

        public virtual void Delete(params Expression<Func<TEntity, bool>>[] conditions)
        {
            IQueryable<TEntity> objects = dbSet.AsQueryable();

            foreach (var condition in conditions)
            {
                objects = objects.Where(condition);
            }
            foreach (var obj in objects)
            {
                dbSet.Remove(obj);
            }
        }

        public virtual async Task DeleteAsync(params Expression<Func<TEntity, bool>>[] conditions)
        {
            IQueryable<TEntity> objects = await Task.Run(() => dbSet.AsQueryable());

            foreach (var condition in conditions)
            {
                objects = objects.Where(condition);
            }
            foreach (var obj in objects)
            {
                await Task.Run(() => dbSet.Remove(obj));
            }
        }

        public virtual TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual TEntity GetById(string id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(string id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate).AsEnumerable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where)
        {
            return await dbSet.Where(where).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return await dbSet.Where(where).FirstOrDefaultAsync();
        }
    }
}
