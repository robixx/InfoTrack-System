using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.DAO
{
    public class AuthenticationDAO : IAuth
    {
        private readonly DatabaseConnection _connection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticationDAO(DatabaseConnection connection, IHttpContextAccessor httpContextAccessor)
        {
            _connection = connection;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest login)
        {
            try
            {
                if (login != null)
                { 
               
                    var user = await _connection.Users
                        .Where(e => e.LoginName == login.UserName && e.Password == login.Password).FirstOrDefaultAsync();

                    if (user == null)
                        return null; 

                   
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Role, "SuperAdmin"),
                            new Claim("UserId", user.UserId.ToString()),
                            new Claim("RoleId", "1")
                        };

                    var identity = new ClaimsIdentity(claims, "CookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    var httpContext = _httpContextAccessor.HttpContext;
                    if (httpContext != null)
                    {
                        await httpContext.SignInAsync("CookieAuth", principal);
                    }

                    return new LoginResponse
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        RoleName = "SuperAdmin",
                        RoleId = 1
                    };
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
