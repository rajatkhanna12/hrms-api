using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models.EntitesObjects
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserCode { get; set; }
        public int? DepartmentId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string? Email { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
    }
}
