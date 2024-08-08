using HRMS.Models;
using HRMS.Models.EntitesObjects;
using HRMS.Models.ViewModels;

namespace HRMS.Core.IRepository
{
    public interface IUserService
    {
        public JsonResultModel<UserDto> GetUserById(int id);
        Task<JsonResultModel<UserRole>> AddRole(UserRole model);
        Task<JsonResultModel<UserAttendance>> SaveAttendance(UserAttendanceDto model);
        Task<JsonResultModelList<UserAttendance>> GetWeeklyAttendanceByUserId(int userId);
    }
}
