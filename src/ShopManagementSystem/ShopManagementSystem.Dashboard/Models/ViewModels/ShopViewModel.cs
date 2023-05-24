namespace ShopManagementSystem.Dashboard.Models.ViewModels;

public partial class ShopViewModel
{
    public record ShopView
    {
        public Guid ShopId { get; set; }

        public string Title { get; set; }
        public string Phone { get; set; }
    }

    public record ShopDetail
    {
        public Guid ShopId { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public List<ProductViewModel.ProductView> Products { get; set; } = new();
        public List<EmployeeViewModel.EmployeeView> Employees { get; set; } = new();
    }
}