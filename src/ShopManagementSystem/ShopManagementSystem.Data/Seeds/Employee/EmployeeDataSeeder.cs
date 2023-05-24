using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Data.Seeds;

public sealed class EmployeeDataSeeder
{
    public static void SeedEmployeeData(ApplicationDbContext context)
    {
        if (context.Employees.Any())
        {
            return;
        }
        
        var employees = new List<Employee>
        {
            new() { FirstName = "Clark", LastName = "Kent", Post = "Seller", UserId = Guid.Parse("1A032BFD-8FCC-42EC-8B59-4233B63FB939"), ShopId = Guid.Parse("4F8B0BED-DF39-47C9-9A6D-87BD6917CDAE")}
        };
        context.Employees.AddRange(employees);
    }
}