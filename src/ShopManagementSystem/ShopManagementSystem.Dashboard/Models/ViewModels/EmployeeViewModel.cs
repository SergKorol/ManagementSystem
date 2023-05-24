namespace ShopManagementSystem.Dashboard.Models.ViewModels;

public partial class EmployeeViewModel
{
    public record EmployeeView
    {
        public Guid EmployeeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public string Post { get; set; }
    }
}