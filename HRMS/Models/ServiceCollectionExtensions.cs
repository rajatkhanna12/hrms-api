using HRMS.Core.IRepository;
using HRMS.Core.Repository;

namespace HRMS.Models
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TaskService>();
            return services;
        }
    }
}
