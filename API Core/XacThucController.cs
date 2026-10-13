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

namespace API_Core.Controllers
{
    [Route("api/xac-thuc")]
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
            var user = _userBusiness.ValidateUser(request.TenDangNhap, request.MatKhau);

            if (user == null)
            {
                return Unauthorized(new { message = "Sai thông tin đăng nhập hoặc tài khoản không tồn tại!" });
            }

            var token = GenerateJwtToken(user, config);

            return Ok(new { token = token, role = user.VaiTro, message = "Đăng nhập thành công" });
        }

        private string GenerateJwtToken(LoggedInUser user, IConfiguration config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.TenDangNhap),
                new Claim(ClaimTypes.Role, user.VaiTro)
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