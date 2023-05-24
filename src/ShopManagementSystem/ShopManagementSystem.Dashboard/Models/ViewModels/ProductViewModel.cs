namespace ShopManagementSystem.Dashboard.Models.ViewModels;

public partial class ProductViewModel
{
    public record ProductView
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }
    }
    
    public record ProductDetail : ProductView
    {}
}