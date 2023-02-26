using HRM.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Implementations
{
    public class GenericeRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public GenericeRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public void Add(T item)
        {
           dbSet.Add(item);
        }

        public async Task<T> AddAsync(T item)
        {
            await dbSet.AddAsync(item);
            return item;
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Delete(T item)
        {
            if (_context.Entry(item).State == EntityState.Detached)
                dbSet.Attach(item);

            dbSet.Remove(item); 

        }

        public async Task<T> DeleteAsync(T item)
        {
            if (_context.Entry(item).State == EntityState.Detached)
                dbSet.Attach(item);

            dbSet.Remove(item);
       
            return  item;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(includeProperty);
            }
            if (orderBy != null)
                return orderBy(query).ToList();
            else 
                return query.ToList();
        }

        public IEnumerable<T> GetAllWithFilter(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties)
            {
                query.Include(includeProperty);
            }
            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }
        private bool disposed = false;
        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                   _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Update(T item)
        {
            dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;

        }
         
        public async Task<T> UpdateAsync(T item)
        {
            dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }
        public void AddRange(ICollection<T> items)
        {
            dbSet.AddRange(items);
        }
        public IQueryable<T> Get()
        {
            return dbSet;
        }
    }
}
