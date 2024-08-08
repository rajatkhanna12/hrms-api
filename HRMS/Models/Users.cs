using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? UserCode { get; set; }
        public int? DepartmentId { get; set; }
        public string? Name { get; set; }
        public DateTime? LastPasswordUpdated { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? PhoneNumber { get; set; }

        [ForeignKey("UserRole")]
        public int RoleId { get; set; }
        public string? Email { get; set; }

        [NotMapped]
        public string Token { get; set; }
        public bool IsActive { get; set; }

        public UserRole UserRole { get; set; } 

    }
}
