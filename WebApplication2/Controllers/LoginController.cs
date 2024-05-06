using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController:ControllerBase
    {
        [HttpGet]
        public async Task<string> LoginAsync()
        {
            var claims = new List<Claim>()
           {
                new Claim(ClaimTypes.Name, "zhangsan"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDI2a2EJ7m872v0afyoSDJT2o1+SitIeJSWtLJU8/Wz2m7gStexajkeD+Lka6DSTy8gt9UwfgVQo6uKjVLG5Ex7PiGOODVqAEghBuS7JzIYU5RvI543nNDAPfnJsas96mSA7L/mD7RTE2drj6hf3oZjJpMPZUQI/B1Qjb5H3K3PNwIDAQAB"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7165",
                audience: "Ousu_Xld_Platform",
                claims: claims,
                expires: DateTime.Now.AddDays(300),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
