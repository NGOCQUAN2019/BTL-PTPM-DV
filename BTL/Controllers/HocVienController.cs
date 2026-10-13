using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Model;
using System;
using BLL.Interfaces;

namespace API_Admin.Controllers // Đổi namespace cho khớp với project
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "QuanTri")] // Bổ sung thẻ bảo vệ API chỉ cho Admin
    public class HocVienController : ControllerBase
    {
        private readonly IHocVienBusiness _studentBusiness;

        public HocVienController(IHocVienBusiness studentBusiness)
        {
            _studentBusiness = studentBusiness;
        }

        [HttpPost("create")]
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

        // Đổi studentCode thành maHocVien trên đường dẫn
        [HttpDelete("delete/{maHocVien}")]
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

        // Đổi studentCode thành maHocVien trên đường dẫn
        [HttpGet("get-by-id/{maHocVien}")]
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
        public IActionResult SearchStudents([FromQuery] string tuKhoa = "") // Đổi keyword thành tuKhoa
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