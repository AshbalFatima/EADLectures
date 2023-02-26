using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Apis.Common
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {

        private readonly IMongoCollection<T> _dbcollections;
        private readonly FilterDefinitionBuilder<T> filter = Builders<T>.Filter;
        
        public Repository(IMongoDatabase database, string collectionName)
        {
            _dbcollections = database.GetCollection<T>(collectionName);
        }
        public void Create(T entity)
        {
            if (entity != null) ;
                _dbcollections.InsertOne(entity);
        }

        public void Delete(Guid id)
        {
             _dbcollections.DeleteOne(filter.Eq(t => t.Id, id));
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return _dbcollections.Find(filter.Empty).ToList();
        }

        public T GetById(Guid id)
        {
            return _dbcollections.Find(filter.Eq(t => t.Id, id)).FirstOrDefault();
        }

        public void Update(T entity)
        {
            _dbcollections.ReplaceOne(filter.Eq(t => t.Id, entity.Id), entity);
        }
    }
}
