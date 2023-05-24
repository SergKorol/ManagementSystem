using Microsoft.EntityFrameworkCore;
using ShopManagementSystem.Data.Context;

namespace ShopManagementSystem.Data.Repository;

public class Repository<T> : IRepository<T> where T : class, new()
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id) ?? new T();
    }

    public async Task<IEnumerable<T>> GetByListId(List<Guid> ids, string property)
    {
        return _context.Set<T>().ToList().Where(item => ids.Contains(GetIdFromItem<T>(item, property)));
    }

    private Guid GetIdFromItem<T>(T item, string property) where T : class
    {
        var idProperty = typeof(T).GetProperty(property);
        return (Guid)idProperty.GetValue(item);
    }
    
    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }
}