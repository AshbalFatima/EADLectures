using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Interfaces
{
    public interface IGenericRepository <T> : IDisposable
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        void Add(T item);
        void AddRange(ICollection<T> items);
        Task<T> AddAsync(T item);
        void Update(T item);    
        Task<T> UpdateAsync(T item);
        void Delete(T item);
        Task<T> DeleteAsync(T item);
        
        IQueryable<T> Get();

        IEnumerable<T> GetAllWithFilter(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties);
        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        //{
        //    IQueryable<TEntity> query = conn.Set<TEntity>();
        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }
        //    return query;
        //}
    }
}
