using AutoMapper;
using HRMS.Core.IRepository;
using HRMS.Extensions;
using HRMS.Models;
using HRMS.Models.EntitesObjects;
using HRMS.Models.ViewModels;
using HRMS.Utilities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Core.Repository
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper mapper;
        public UserService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            this.mapper = mapper;
        }
        public JsonResultModel<UserDto> GetUserById(int id)
        {
            try
            {
                var user = _dataContext.Users.FirstOrDefault(m => m.UserId == id);
                if (user == null)
                {
                    return MethodsGen<UserDto>.GetObject(true, null, "User not exists.");
                }
                else
                {
                    var result = mapper.Map<UserDto>(user);
                    return MethodsGen<UserDto>.GetObject(false, result, "");
                }
            }
            catch (Exception ex)
            {
                return MethodsGen<UserDto>.GetObject(true, null, ex.Message);
            }

        }
        
        public async Task<JsonResultModel<UserRole>> AddRole(UserRole model)
        {
            if(string.IsNullOrEmpty(model.RoleName))
                return MethodsGen<UserRole>.GetObject(true, model, "Role name is required");

            if (_dataContext.UserRoles.Any(m=>m.RoleName.ToLower() == model.RoleName.ToString()))
                return MethodsGen<UserRole>.GetObject(true, model, "Role already exists");
            _dataContext.UserRoles.Add(model);
            _dataContext.SaveChanges();
            return MethodsGen<UserRole>.GetObject(false, model, "Role added successfully");
        }

        public async Task<JsonResultModel<UserAttendance>> SaveAttendance(UserAttendanceDto model)
        {
            try
            {
                if (_dataContext.Users.Any(m => m.UserId == model.UserId))
                {
                    var attendence = new UserAttendance()
                    {
                        UserId = model.UserId,
                        PunchIn = model.PunchIn,
                        PunchOut = model.PunchOut,
                        TotalWorkHours = (model.PunchOut - model.PunchIn),
                    };
                    _dataContext.UserAttendances.Add(attendence);
                    _dataContext.SaveChanges();
                    return MethodsGen<UserAttendance>.GetObject(false, attendence, "Attendance added successfully.");
                }
                else
                {

                    return MethodsGen<UserAttendance>.GetObject(true, new UserAttendance() { PunchIn = model.PunchIn, PunchOut = model.PunchOut }, "Error occurred while saving the attendance.");
                }
            }
            catch (Exception ex)
            {
                return MethodsGen<UserAttendance>.GetObject(true, new UserAttendance() { PunchIn = model.PunchIn, PunchOut = model.PunchOut }, ex.Message);
            }
        }


        public async Task<JsonResultModelList<UserAttendance>> GetWeeklyAttendanceByUserId(int userId)
        {
            try
            {
                var currentDate = DateTime.Now;
                var startDate = currentDate.StartOfWeek(DayOfWeek.Monday);
                var result = await _dataContext.UserAttendances.AsNoTracking().Where(m => m.UserId == userId && m.PunchIn >= startDate && m.PunchOut <= currentDate).OrderByDescending(m=>m.Id).ToListAsync();

                return new JsonResultModelList<UserAttendance>()
                {
                    IsError = false,
                    Data = result,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new JsonResultModelList<UserAttendance>()
                {
                    IsError = true,
                    Data = null,
                    Message = ex.Message
                };
            }
        }
    }
}
