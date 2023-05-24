using System.Reflection;
using Mapster;
using ShopManagementSystem.Application.DTOs;
using ShopManagementSystem.Dashboard.Models.ViewModels;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Dashboard.Configuration;

public static class MapsterConfig
{
    public static void DashboardMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<ShopDto, ShopViewModel.ShopDetail>
            .NewConfig()
            .Map(dest => dest.Products, src => src.Products != null ? src.Products : new List<ProductDto>())
            .Map(dest => dest.Employees, src => src.Employees != null ? src.Employees : new List<EmployeeDto>());;
        
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}