using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class UserAttendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public Users? User { get; set; }
        public DateTime PunchIn { get; set; }
        public DateTime PunchOut { get; set; }
        public TimeSpan TotalWorkHours { get; set; }

        
    }
}
