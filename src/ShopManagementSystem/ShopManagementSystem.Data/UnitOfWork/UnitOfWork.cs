using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Models;
using ShopManagementSystem.Data.Repository;

namespace ShopManagementSystem.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private bool _disposed;
    
    public IRepository<User> UserRepository { get; private set; }
    public IRepository<Shop> ShopRepository { get; private set; }
    public IRepository<Product> ProductRepository { get; private set; }
    public IRepository<Employee> EmployeeRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        UserRepository = new Repository<User>(_context);
        ShopRepository = new Repository<Shop>(_context);
        ProductRepository = new Repository<Product>(_context);
        EmployeeRepository = new Repository<Employee>(_context);
    }
  
    
    
    public void Save()
    {
        _context.SaveChanges();
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}