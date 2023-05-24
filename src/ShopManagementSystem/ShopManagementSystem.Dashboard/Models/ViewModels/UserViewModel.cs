namespace ShopManagementSystem.Dashboard.Models.ViewModels;

public partial class UserViewModel
{
    public record UserView
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public record UserDetailView
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}

