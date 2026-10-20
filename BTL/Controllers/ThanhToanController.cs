using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL.Interfaces;

namespace API_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "QuanTri")] // Chỉ Admin mới được thu tiền
    public class ThanhToanController : ControllerBase
    {
        private readonly IThanhToanBusiness _thanhToanBusiness;

        public ThanhToanController(IThanhToanBusiness thanhToanBusiness)
        {
            _thanhToanBusiness = thanhToanBusiness;
        }

        [HttpPost("thu-tien")]
        public IActionResult ThuTien([FromBody] ThanhToanRequest request)
        {
            if (request.SoTien <= 0)
                return BadRequest(new { message = "Số tiền thanh toán phải lớn hơn 0." });

            try
            {
                if (_thanhToanBusiness.ThemThanhToan(request))
                    return Ok(new { message = "Thanh toán thành công! Trạng thái hóa đơn và ghi danh đã được cập nhật." });

                return BadRequest(new { message = "Giao dịch thất bại." });
            }
            catch (Exception ex)
            {
                // Bắt lỗi từ SQL (ví dụ sai mã hóa đơn)
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}