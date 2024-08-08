using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models.EntitesObjects
{
    public class SaveUserDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? UserCode { get; set; }
        public int? DepartmentId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string? Email { get; set; }

    }
}
