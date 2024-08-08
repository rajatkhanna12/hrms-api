using AutoMapper;
using HRMS.Core.IRepository;
using HRMS.Models;
using HRMS.Models.EntitesObjects;
using HRMS.Models.ViewModels;
using HRMS.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRMS.Core.Repository
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper mapper;
        public AuthService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<JsonResultModel<Users>> Login( LoginModel login)
        {
            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
                return MethodsGen<Users>.GetObject(true, null, "Username and password are mandatory");
            var user = _dataContext.Users.Include(m=>m.UserRole).FirstOrDefault(m => m.Password == Methods.AESEncryption.EncryptString(login.Password) && m.UserName.ToLower() == login.UserName.ToLower());

            if (user is not null)
            {
                user.Token = GenerateJwtToken(user);
                return MethodsGen<Users>.GetObject(false, user, "Login successful");
            }


            else
                return MethodsGen<Users>.GetObject(true, null, "User is not exists");


        }


        public async Task<JsonResultModel<SaveUserDto>> SaveUser(SaveUserDto user)
        {

            try
            {
                var validUser = ValidateUser(user);
                if (validUser.IsError) return validUser;
                user.Password = Methods.AESEncryption.EncryptString(user.Password ?? "");
                var model = mapper.Map<Users>(user);
                model.IsActive = true;
                _dataContext.Users.Add(model);
                _dataContext.SaveChanges();
                return MethodsGen<SaveUserDto>.GetObject(false, user, "User saved successfully");
            }
            catch (Exception ex)
            {
                return MethodsGen<SaveUserDto>.GetObject(true, user, ex.Message);

            }

        }

        public JsonResultModel<SaveUserDto> ValidateUser(SaveUserDto user)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.UserName))
                {
                    return MethodsGen<SaveUserDto>.GetObject(true, user, "Username and password is mandatory");

                }
                else if (_dataContext.Users.Any(m => m.UserName.ToLower() == user.UserName.ToLower()))
                {
                    return MethodsGen<SaveUserDto>.GetObject(true, user, "User name already exists.");

                }
                return new JsonResultModel<SaveUserDto>()
                {
                    IsError = false
                };
            }
            catch (Exception ex)
            {
                return MethodsGen<SaveUserDto>.GetObject(true, user, ex.Message);
            }

        }

        private string GenerateJwtToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMXMX"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Role, user.UserRole.RoleName)

                // Add additional claims as needed
    };
            var token = new JwtSecurityToken(
                issuer: "HRMS",
                audience: "HRMSAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
