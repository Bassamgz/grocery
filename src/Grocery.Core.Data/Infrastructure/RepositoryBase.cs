namespace Grocery.Core.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public abstract class RepositoryBase<T>
        where T : class
    {
        private readonly DbSet<T> dbSet;

        private eFruitEntities dataContext;

        protected RepositoryBase(IDbFactory dbFactory)
        {
            this.DbFactory = dbFactory;
            this.dbSet = this.DbContext.Set<T>();
        }

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected eFruitEntities DbContext
        {
            get { return this.dataContext ?? (this.dataContext = this.DbFactory.Init()); }
        }

        public virtual void Add(T entity)
        {
            this.dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            this.dbSet.Attach(entity);
            this.dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = this.dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                this.dbSet.Remove(obj);
            }
        }

        public virtual T GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return this.dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return this.dbSet.Where(where).FirstOrDefault<T>();
        }
    }
}
