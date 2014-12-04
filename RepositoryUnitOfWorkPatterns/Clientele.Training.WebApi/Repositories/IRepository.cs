using System;
using System.Linq;
using System.Linq.Expressions;

namespace Clientele.Training.WebApi.Repositories
{
    public interface IRepository<T> where T : class 
    {
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Save();
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(Expression<Func<T, bool>> predicate);
    }
}