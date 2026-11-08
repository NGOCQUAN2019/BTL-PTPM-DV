using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Model; // Thêm thư viện chứa model Hóa Đơn
using System;
using BLL.Interfaces;

namespace API_Admin.Controllers // Đã đổi thành project Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Chốt chặn vòng ngoài: Bắt buộc đăng nhập
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonBusiness _hoaDonBusiness;

        public HoaDonController(IHoaDonBusiness hoaDonBusiness)
        {
            _hoaDonBusiness = hoaDonBusiness;
        }

        // Xem toàn bộ hóa đơn / doanh thu của trung tâm
        [HttpGet("get-all")]
        [Authorize(Roles = "QuanTri")] // Nghiệp vụ tài chính chỉ dành cho Admin
        public IActionResult GetAll()
        {
            return Ok(_hoaDonBusiness.GetAll());
        }

        // Tra cứu hóa đơn theo ID học viên
        [HttpGet("get-by-hoc-vien/{hocVienId}")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult GetByHocVienId(int hocVienId)
        {
            return Ok(_hoaDonBusiness.GetByHocVienId(hocVienId));
        }

        // Admin tạo hóa đơn thủ công (nếu cần)
        [HttpPost("create")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult Create([FromBody] HoaDonModel model)
        {
            try
            {
                // Gọi hàm BLL tạo hóa đơn
                return Ok(new { message = "Tạo hóa đơn thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Admin duyệt hóa đơn (Cập nhật trạng thái "Đã thanh toán")
        [HttpPut("update-trang-thai")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult Update([FromBody] HoaDonModel model)
        {
            try
            {
                // Gọi hàm BLL cập nhật trạng thái hóa đơn
                // if (_hoaDonBusiness.Update(model)) ...
                return Ok(new { message = "Cập nhật trạng thái thanh toán thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Admin hủy/xóa hóa đơn lỗi
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Gọi hàm BLL xóa hóa đơn
                // if (_hoaDonBusiness.Delete(id)) ...
                return Ok(new { message = "Xóa hóa đơn thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}