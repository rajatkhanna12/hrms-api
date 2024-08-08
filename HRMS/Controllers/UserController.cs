using HRMS.Core.IRepository;
using HRMS.Models;
using HRMS.Models.EntitesObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("/GetUserById")]
        public JsonResult GetUserById(int userId)
        {
            return Json(_userService.GetUserById(userId));
        }

        [HttpPost]
        [Route("/AddNewRole")]
        public JsonResult AddNewRole(UserRole model)
        {
           return Json(_userService.AddRole(model));
        }

        [HttpPost]
        [Route("/SaveAttendance")]
        public async Task<JsonResult>  SaveAttendance(UserAttendanceDto model)
        {
            return Json(await _userService.SaveAttendance(model));
        }

        [HttpGet]
        [Route("/GetAttendancesByUserId")]

        public async Task<JsonResult> GetAttendancesByUserId(int userId)
        {
            return Json(await _userService.GetWeeklyAttendanceByUserId(userId));
        }
    }
}
