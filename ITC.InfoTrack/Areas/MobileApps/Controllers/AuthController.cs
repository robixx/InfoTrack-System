using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using ITC.InfoTrack.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITC.InfoTrack.Areas.MobileApps.Controllers
{
    [Area("api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuth _auth;
        private readonly JwtConfig _jwtConfig;
        private readonly string _secretCode;

        public AuthController(IAuth auth, JwtConfig jwtConfig, IConfiguration configuration)
        {
            _auth = auth;
            _jwtConfig = jwtConfig;
            _secretCode = configuration["Secret-Key:X-Secret_Key"];
        }

        [HttpGet("Login")]
        public async Task<IActionResult> LogIn([FromBody] LoginRequest request)
        {

            if (request == null)
            {
                var jsonData = new
                {
                    code = "108",
                    message = "Invalid username/password",
                    token = ""
                };
                return Unauthorized(jsonData);
            }

            if (!Request.Headers.TryGetValue("X-Secret-Key", out var secretKey))
            {
                return Unauthorized(new
                {
                    code = "401",
                    message = "Missing secret key",
                    token = ""
                });
            }

            if (secretKey != _secretCode)
            {
                return Unauthorized(new
                {
                    code = "401",
                    message = "Invalid secret key",
                    token = ""
                });
            }


            var response = await _auth.ApiLoginAsync(request);

            if (response != null && response.UserId > 0)
            {
                JwtUser jwt = new()
                {
                    UserId = response.UserId,
                    DispalyName = response.UserName,
                    RoleId = response.RoleId,
                    RoleName = response.RoleName,
                    TokenExpired = DateTime.Now.AddMinutes(5)
                };

                if (jwt != null)
                {
                    string strToken = _jwtConfig.Generate(jwt);
                    var jsonData = new
                    {
                        code = "200",
                        message = "Login Successfully",
                        token = strToken
                    };

                    return Ok(jsonData);
                }
                else
                {
                    var jsonData = new
                    {
                        code = "108",
                        message = "Invalid username/password",
                        token = ""
                    };
                    return Unauthorized(jsonData);
                }
            }
            else
            {
                var jsonData = new
                {
                    code = "108",
                    message = "Invalid username/password",
                    token = ""
                };
                return Unauthorized(jsonData);
            }

        }


    }
}
