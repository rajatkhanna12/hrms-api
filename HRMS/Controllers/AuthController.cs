using HRMS.Core.IRepository;
using HRMS.Models;
using HRMS.Models.EntitesObjects;
using HRMS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("/SaveUser")]

        public async Task<JsonResult> SaveUser(SaveUserDto registerUser)
        {
            return Json(await _authService.SaveUser(registerUser)) ;
        }

        [HttpPost]
        [Route("/Login")]
        [AllowAnonymous]
        public async Task<JsonResult> Login(LoginModel login)
        {
            return Json(await _authService.Login(login));
        }

      
    }
}
