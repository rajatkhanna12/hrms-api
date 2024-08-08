using AutoMapper;
using HRMS.Core.IRepository;
using HRMS.Models;
using HRMS.Models.ViewModels;
using HRMS.Utilities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Core.Repository
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper mapper;
        public TaskService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<JsonResultModel<WorkDiary>> CreateTask(WorkDiary workDiary)
        {

            try
            {
                var isExists = _dataContext.WorkDiaries.Any(m => m.Id == workDiary.Id);
                if (workDiary.Id == 0 && !isExists)
                {
                    await _dataContext.WorkDiaries.AddAsync(workDiary);
                    await _dataContext.SaveChangesAsync();
                }
                else
                {
                    return MethodsGen<WorkDiary>.GetObject(true, workDiary, "Task already exists");
                }
                return MethodsGen<WorkDiary>.GetObject(false, workDiary, "Successfully task created");
            }
            catch (Exception ex)
            {
                return MethodsGen<WorkDiary>.GetObject(true, workDiary, ex.Message);
            }
        }


        public async Task<JsonResultModel<WorkDiary>> UpdateTask(int taskId, int status)
        {

            try
            {
                var task = await _dataContext.WorkDiaries.FirstOrDefaultAsync(m => m.Id == taskId);
                if (task != null)
                {
                    task.Status = status;
                    _dataContext.WorkDiaries.Update(task);
                    await _dataContext.SaveChangesAsync();
                }
                else
                {
                    return MethodsGen<WorkDiary>.GetObject(true, task, "Task does not exist");
                }
                return MethodsGen<WorkDiary>.GetObject(false, null, "Successfully task created");
            }
            catch (Exception ex)
            {
                return MethodsGen<WorkDiary>.GetObject(true, null, ex.Message);
            }
        }
    }
}
