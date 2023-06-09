using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShopManagementSystem.Data.Models;
using ShopManagementSystem.Data.Repository;

namespace ShopManagementSystem.Data;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> UserRepository { get; }
    IRepository<Shop> ShopRepository { get; }
    IRepository<Product> ProductRepository { get; }
    IRepository<Employee> EmployeeRepository { get; }
    IRepository<ShopProduct> ShopProductRepository { get; }
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    void Save();
}