using System.Text.Json.Serialization;

namespace HRMS.Models
{
    public class UserRole
    {
        [JsonIgnore]
        public int Id { get; set; } 
        public string RoleName { get; set; } = string.Empty;
    }
}
