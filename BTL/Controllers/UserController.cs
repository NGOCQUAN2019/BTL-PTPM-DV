using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Model;
using BLL.Interfaces;

namespace API_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel request, [FromServices] IConfiguration config)
        {
            var user = _userBusiness.ValidateUser(request.Username, request.Password);

            // Gom chung lỗi sai thông tin và sai quyền hạn bằng 1 câu thông báo chuyên nghiệp
            if (user == null || user.Role != "admin")
            {
                return Unauthorized(new { message = "Sai tài khoản, mật khẩu hoặc bạn không có quyền truy cập!" });
            }

            var token = GenerateJwtToken(user, config);
            return Ok(new { token = token, role = user.Role, message = "Đăng nhập thành công" });
        }

        private string GenerateJwtToken(LoggedInUser user, IConfiguration config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}