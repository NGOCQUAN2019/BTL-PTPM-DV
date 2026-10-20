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
        private readonly IThanhToanBusiness _thanhToanBusiness;

        public UHoaDonController(IThanhToanBusiness thanhToanBusiness)
        {
            _thanhToanBusiness = thanhToanBusiness;
        }

        [HttpGet("my-invoices")]
        public IActionResult GetMyInvoices()
        {
            // Tự động trích xuất Tên đăng nhập (hoặc Id) của Học viên từ Token JWT
            // Giả sử claim NameIdentifier trong Token của bạn đang lưu HocVienId
            var currentUserIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentUserIdStr) || !int.TryParse(currentUserIdStr, out int hocVienId))
            {
                return Unauthorized(new { message = "Không xác định được danh tính học viên." });
            }

            // Gọi xuống BLL/DAL để lấy hóa đơn của đúng học viên này
            var invoices = _thanhToanBusiness.GetHoaDonByHocVien(hocVienId);
            return Ok(invoices);
        }
    }
}