using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Model;
using System;
using BLL.Interfaces;

namespace API_Admin.Controllers
{
    // SỬA 1: Đổi thành admin-api cho khớp với Frontend
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HocVienController : ControllerBase
    {
        private readonly IHocVienBusiness _studentBusiness;

        public HocVienController(IHocVienBusiness studentBusiness)
        {
            _studentBusiness = studentBusiness;
        }

        // SỬA 2: Thêm hàm get-all để Frontend tải danh sách lên bảng
        [HttpGet("get-all")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult GetAllHocVien()
        {
            try
            {
                // Dùng luôn hàm Search với từ khóa rỗng để lấy tất cả
                var data = _studentBusiness.SearchStudents("");
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult CreateStudent([FromBody] HocVienModel model)
        {
            try
            {
                bool result = _studentBusiness.CreateStudent(model);
                if (result) return Ok(new { statusCode = 200, message = "Thêm thành công!" });
                return BadRequest(new { statusCode = 400, message = "Thêm thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult UpdateStudent([FromBody] HocVienModel model)
        {
            try
            {
                _studentBusiness.UpdateStudent(model);
                return Ok(new { statusCode = 200, message = "Cập nhật thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        // Đã giữ nguyên dùng maHocVien (chữ) theo thiết kế của bạn
        [HttpDelete("delete/{maHocVien}")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult DeleteStudent(string maHocVien)
        {
            try
            {
                _studentBusiness.DeleteStudent(maHocVien);
                return Ok(new { statusCode = 200, message = "Xóa thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        // Đã giữ nguyên dùng maHocVien (chữ) theo thiết kế của bạn
        [HttpGet("get-by-id/{maHocVien}")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult GetStudentById(string maHocVien)
        {
            try
            {
                var data = _studentBusiness.GetStudentById(maHocVien);
                if (data == null) return NotFound(new { message = "Không tìm thấy học viên!" });
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("search")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult SearchStudents([FromQuery] string tuKhoa = "")
        {
            try
            {
                var data = _studentBusiness.SearchStudents(tuKhoa);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}