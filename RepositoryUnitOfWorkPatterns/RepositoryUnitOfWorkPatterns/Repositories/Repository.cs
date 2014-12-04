using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Linq;
using RepositoryUnitOfWorkPatterns.Models;


namespace RepositoryUnitOfWorkPatterns.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal TrainingDataContext dataContext;
        internal DbSet<T> dbSet;

        public Repository(TrainingDataContext context)
        {
            dataContext = context;
            dbSet = context.Set<T>();
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (dataContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public void Update(T enity)
        {
            dbSet.Attach(enity);
            dataContext.Entry(enity).State = EntityState.Modified;
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = dbSet.Where(predicate);
            return query;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = dataContext.Set<T>();
            return query;
        }

        public virtual void Save()
        {
            dataContext.SaveChanges();
        }

        public T GetById(Expression<Func<T, bool>> predicate)
        {
            var entity = dataContext.Set<T>().Where(predicate).FirstOrDefault();
            return entity;

        }
    }
}