using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Data.Seeds;

public class EmployeeDataSeeder
{
    public static void SeedEmployeeData(ApplicationDbContext context)
    {
        if (context.Employees.Any())
        {
            return;
        }
        
        var employees = new List<Employee>
        {
            new Employee { FirstName = "John", LastName = "Rambo", Post = "Seller", UserId = Guid.Parse("A16CBD8A-CCD9-40D6-A0AF-3845D5E7B5C2")},
            new Employee { FirstName = "Jack", LastName = "Sparrow", Post = "Seller", UserId = Guid.Parse("CA6246C6-93C0-46CD-88BB-0267384DD840")},
            new Employee { FirstName = "Clark", LastName = "Kent", Post = "Seller", UserId = Guid.Parse("1A032BFD-8FCC-42EC-8B59-4233B63FB939")}
        };
        context.Employees.AddRange(employees);
    }
}