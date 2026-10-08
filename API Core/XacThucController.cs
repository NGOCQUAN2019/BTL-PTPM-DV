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

namespace API_Core.Controllers // Đã đổi thành API_Core
{
    [Route("api/xac-thuc")] // Đổi route tiếng Việt cho đẹp
    [ApiController]
    public class XacThucController : ControllerBase
    {
        private readonly INguoiDungBusiness _userBusiness;

        public XacThucController(INguoiDungBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel request, [FromServices] IConfiguration config)
        {
            var user = _userBusiness.ValidateUser(request.Username, request.Password);

            // Bỏ kiểm tra Role, chỉ kiểm tra xem tài khoản/mật khẩu có đúng không
            if (user == null)
            {
                return Unauthorized(new { message = "Sai thông tin đăng nhập hoặc tài khoản không tồn tại!" });
            }

            var token = GenerateJwtToken(user, config);

            // Trả về cả token và role để phía Front-end (React/Vue/Angular) biết đường điều hướng trang web
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