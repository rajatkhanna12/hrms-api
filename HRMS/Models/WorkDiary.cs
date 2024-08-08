using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HRMS.Models
{
    public class WorkDiary
    {
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Required]
        public string TaskTitle { get; set; }

        public string TaskDescription { get; set; } 
        public Double EstimatedHours { get; set; }  

        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdateDateTime { get; set; }

    }
}
