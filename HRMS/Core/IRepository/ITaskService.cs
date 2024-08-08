using HRMS.Models;
using HRMS.Models.ViewModels;

namespace HRMS.Core.IRepository
{
    public interface ITaskService
    {
        Task<JsonResultModel<WorkDiary>> CreateTask(WorkDiary workDiary);
        Task<List<WorkDiary>> GetTasks();
        Task<JsonResultModel<WorkDiary>> UpdateTask(int taskId, int status);
    }
}
