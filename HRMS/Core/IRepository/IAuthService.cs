using HRMS.Models;
using HRMS.Models.EntitesObjects;
using HRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Core.IRepository
{
    public interface IAuthService
    {
        public Task<JsonResultModel<SaveUserDto>> SaveUser(SaveUserDto user);
        public Task<List<Users>> GetUsers();
        public Task<List<UserRole>> GetRoles();

        public Task<JsonResultModel<Users>> Login(LoginModel login);
        
    }
}
