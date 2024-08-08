using HRMS.Core.IRepository;
using HRMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [Route("CreateTask")]

        public async Task<JsonResult> CreateTask(WorkDiary model)
        {
            return Json(await _taskService.CreateTask(model));
        }

        [HttpPost]
        [Route("UpdateTask")]

        public async Task<JsonResult> UpdateTask(int taskId, int status)
        {
            return Json(await _taskService.UpdateTask(taskId, status));
        }
    }
}
