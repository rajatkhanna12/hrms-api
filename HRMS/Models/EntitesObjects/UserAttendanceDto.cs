namespace HRMS.Models.EntitesObjects
{
    public class UserAttendanceDto
    {
        public int UserId { get; set; }
        public DateTime PunchIn { get; set; }
        public DateTime PunchOut { get; set; }
    }
}   
