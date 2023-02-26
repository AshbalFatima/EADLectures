namespace Apis.Common
{
    public interface IEntity
    {
        public Guid Id { get; set; }

    }
    public interface IRepository<T> where T : IEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(Guid id);
        T GetById(Guid id);
        IReadOnlyCollection<T> GetAll();

    }
}