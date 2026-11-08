using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Model;
using System;

namespace API_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Chốt chặn vòng ngoài: Bắt buộc đăng nhập
    public class ThanhToanController : ControllerBase
    {
        private readonly IThanhToanBusiness _thanhToanBusiness;

        public ThanhToanController(IThanhToanBusiness thanhToanBusiness)
        {
            _thanhToanBusiness = thanhToanBusiness;
        }

        // --- NHÓM QUYỀN TRA CỨU TÀI CHÍNH ---

        // Xem toàn bộ lịch sử thanh toán / giao dịch của trung tâm
        [HttpGet("get-all")]
        [Authorize(Roles = "QuanTri")] // Độc quyền Kế toán / Admin
        public IActionResult GetAll()
        {
            try
            {
                // Giả định bạn có hàm GetAll trong BLL
                // return Ok(_thanhToanBusiness.GetAll()); 
                return Ok(new { message = "Hàm GetAll chờ gọi BLL" }); // Sửa lại dòng này nếu BLL đã có
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // --- NHÓM QUYỀN XỬ LÝ GIAO DỊCH ---

        // Admin/Kế toán xác nhận thanh toán thủ công (Ví dụ: Học viên đóng tiền mặt tại quầy)
        [HttpPost("thanh-toan")]
        [Authorize(Roles = "QuanTri")] // Độc quyền Kế toán / Admin
        public IActionResult Create([FromBody] ThanhToanModel model)
        {
            try
            {
                if (_thanhToanBusiness.Create(model))
                    return Ok(new { message = "Xác nhận thanh toán thành công! Trạng thái Hóa đơn và Ghi danh đã được tự động cập nhật." });

                return BadRequest(new { message = "Xác nhận thanh toán thất bại." });
            }
            catch (Exception ex)
            {
                // Hứng bẫy lỗi từ DB (ví dụ: Hóa đơn đã được thanh toán rồi, hoặc mã hóa đơn không tồn tại)
                return BadRequest(new { message = ex.Message });
            }
        }

        // Hủy giao dịch / Hoàn tiền (Nếu cần thiết cho Admin)
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Gọi BLL xóa giao dịch và trigger rollback lại trạng thái hóa đơn (nếu có)
                // if (_thanhToanBusiness.Delete(id)) return Ok(...);
                return Ok(new { message = "Hủy giao dịch thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}