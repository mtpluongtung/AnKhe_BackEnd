using System.ComponentModel.DataAnnotations;

namespace ComputerSales.API.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class UpdateRoleDto
    {
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
