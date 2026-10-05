using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Model; // Chứa AuthenticateModel và LoggedInUser
using BLL.Interfaces; // Chứa IUserBusiness

namespace API_Admin.Controllers // Hoặc namespace tương ứng của bạn
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // 1. Khai báo biến _userBusiness
        private readonly IUserBusiness _userBusiness;

        // 2. Hàm khởi tạo (Constructor) để tiêm IUserBusiness vào Controller
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        // 3. Hàm Login của bạn
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel request, [FromServices] IConfiguration config)
        {
            // Lúc này biến _userBusiness đã tồn tại và sẵn sàng gọi xuống BLL
            var user = _userBusiness.ValidateUser(request.Username, request.Password);

            if (user == null) return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu!" });

            var token = GenerateJwtToken(user, config);
            return Ok(new { token = token, role = user.Role, message = "Đăng nhập thành công" });
        }

        // 4. Hàm GenerateJwtToken
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