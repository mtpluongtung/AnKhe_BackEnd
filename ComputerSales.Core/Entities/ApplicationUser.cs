using Microsoft.AspNetCore.Identity;

namespace ComputerSalesAPI.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
