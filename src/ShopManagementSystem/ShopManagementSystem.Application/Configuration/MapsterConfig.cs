using System.Reflection;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using ShopManagementSystem.Application.DTOs;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Application.Configuration;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Shop, ShopDto>
            .NewConfig()
            .Map(dest => dest.Products, src => (src.ShopProducts.Select(x => x.Product)))
            .Map(dest => dest.Employees, src => src.Employees);
        
        TypeAdapterConfig<Product, ProductDto>
            .NewConfig();
        
        // TypeAdapterConfig<Employee, EmployeeDto>
        //     .NewConfig()
        //     .Map(dest => dest.EmployeeId)
         
        
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}