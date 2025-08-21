using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

                    var parameters = new[]
                        {
                            new NpgsqlParameter("p_login", login.UserName ?? (object)DBNull.Value),
                            new NpgsqlParameter("p_password", login.Password ?? (object)DBNull.Value),
                        };

                    var data = await _connection.LoginResponse
                        .FromSqlRaw("SELECT * FROM login_user(@p_login, @p_password)", parameters)
                        .ToListAsync();



                    var user = data.FirstOrDefault();

                    if (user == null)
                        return null;

                    
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Role,user.RoleName),
                            new Claim("UserId", user.UserId.ToString()),
                            new Claim("RoleId",user.RoleId.ToString())
                        };

                    var identity = new ClaimsIdentity(claims, "CookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    var httpContext = _httpContextAccessor.HttpContext;
                    if (httpContext != null)
                    {
                        await httpContext.SignInAsync("CookieAuth", principal);
                    }

                    return user;
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
