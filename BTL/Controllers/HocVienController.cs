using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Model;
using System;
using BLL.Interfaces;

namespace API_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Chốt chặn vòng ngoài: Bắt buộc phải có Token đăng nhập hệ thống
    public class HocVienController : ControllerBase
    {
        private readonly IHocVienBusiness _studentBusiness;

        public HocVienController(IHocVienBusiness studentBusiness)
        {
            _studentBusiness = studentBusiness;
        }

        [HttpPost("create")]
        [Authorize(Roles = "QuanTri")] // Độc quyền Admin
        public IActionResult CreateStudent([FromBody] HocVienModel model)
        {
            try
            {
                bool result = _studentBusiness.CreateStudent(model);
                if (result)
                {
                    return Ok(new { statusCode = 200, message = "Thêm học viên và tạo tài khoản thành công!" });
                }
                return BadRequest(new { statusCode = 400, message = "Thêm học viên thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "QuanTri")] // Độc quyền Admin: Chỉ Admin mới được sửa hồ sơ của học viên bất kỳ
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

        [HttpDelete("delete/{maHocVien}")]
        [Authorize(Roles = "QuanTri")] // Độc quyền Admin
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

        [HttpGet("get-by-id/{maHocVien}")]
        [Authorize(Roles = "QuanTri,GiaoVien")] // Cả Admin và Giáo viên đều được xem chi tiết
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
        [Authorize(Roles = "QuanTri,GiaoVien")] // Cả Admin và Giáo viên đều được tìm kiếm/tra cứu
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