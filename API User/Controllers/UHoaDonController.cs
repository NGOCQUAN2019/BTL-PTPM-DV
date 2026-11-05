using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using System.Security.Claims;
using System.Linq;

namespace APIUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HocVien")] // Chỉ học viên mới xem được
    public class UHoaDonController : ControllerBase
    {
        // 1. Đổi sang dùng IHoaDonBusiness
        private readonly IHoaDonBusiness _hoaDonBusiness;

        // 2. Tiêm IHoaDonBusiness vào Constructor
        public UHoaDonController(IHoaDonBusiness hoaDonBusiness)
        {
            _hoaDonBusiness = hoaDonBusiness;
        }

        [HttpGet("my-invoices")]
        public IActionResult GetMyInvoices()
        {
            // Tự động trích xuất Tên đăng nhập (hoặc Id) của Học viên từ Token JWT
            var currentUserIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentUserIdStr) || !int.TryParse(currentUserIdStr, out int hocVienId))
            {
                return Unauthorized(new { message = "Không xác định được danh tính học viên." });
            }

            // 3. Gọi hàm GetByHocVien từ module Hóa Đơn
            var invoices = _hoaDonBusiness.GetByHocVien(hocVienId);
            return Ok(invoices);
        }
    }
}