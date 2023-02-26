using HRM.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Implementations
{
    public class UnitOfWork :  IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext context) { 
            _dbContext = context;
        }

        public void Dispose()
        {
         Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool _disposed;
        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            { 
                if(disposing)
                    _dbContext.Dispose();
            }
            _disposed = true;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            IGenericRepository<T> repo = new GenericeRepository<T>(_dbContext);
            return repo;
        }

        public  void Save()
        {
        
             _dbContext.SaveChanges();
            
        }

        public bool IsBusy()
        {
            var transaction = _dbContext.Database.CurrentTransaction;
            return transaction != null;
        }
        //public bool IsBusy()
        //{
        //    var transaction = _dbContext.Database.CurrentTransaction;
        //    return transaction != null;
        //}
    }
}
