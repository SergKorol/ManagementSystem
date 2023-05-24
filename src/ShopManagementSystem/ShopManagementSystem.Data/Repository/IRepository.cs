namespace ShopManagementSystem.Data.Repository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<IEnumerable<T>> GetByListId(List<Guid> ids, string property);
    Task Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}