using ITC.InfoTrack.Model.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITC.InfoTrack.Utility
{
    public class JwtConfig
    {
        private readonly IConfiguration _config;
       
        public JwtConfig(IConfiguration config)
        {
            _config = config;
        }

        public string Generate(JwtUser auth)
        {
            var jti = Guid.NewGuid().ToString();
            var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            try
            {
                double expirationTime = Convert.ToDouble(_config["Expiration:minutes"]);

                auth.TokenExpired = DateTime.UtcNow.AddMinutes(expirationTime);
                var claims = new[] {
                    new Claim(type: "JWTId", jti),
                    new Claim(type: "UserId", value: auth.UserId.ToString() ?? ""),
                    new Claim(type: "DispalyName",value: auth.DispalyName ?? ""),                  
                    new Claim(type: "RoleName",value: auth.RoleName?.ToString() ?? string.Empty),
                    new Claim(type: "RoleId",value: auth.RoleId.ToString()?? "0"),
                    //new Claim(type: "SubUnitName",value: auth.SubUnitName ?? ""),
                    //new Claim(type: "UserStatus",value: auth.UserStatus.ToString() ?? ""),
                    //new Claim(type: "LastPasswordChanged",value: auth.LastPasswordChanged?.ToString("yyyy-MM-dd HH:mm:ss")??""),
                    //new Claim(type: "TokenIssued", value: auth.TokenIssued?.ToString("yyyy-MM-dd HH:mm:ss")??""),
                    new Claim(type: "TokenExpired", value: DateTime.Now.AddMinutes(expirationTime).ToString("yyyy-MM-dd HH:mm:ss")??"")

                };


                var token = new JwtSecurityToken(
                        _config["Jwt:Issuer"],
                        _config["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(expirationTime),
                        signingCredentials: credentials);

                //_jwtStore.StoreJti(jti, DateTime.UtcNow.AddHours(expirationTime));

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {

                throw new Exception("Error generating token", ex);
            }

        }
    }
}
