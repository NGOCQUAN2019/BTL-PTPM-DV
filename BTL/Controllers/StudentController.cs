using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using System;
using BLL.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentBusiness _studentBusiness;

        public StudentController(IStudentBusiness studentBusiness)
        {
            _studentBusiness = studentBusiness;
        }

        [HttpPost("create")]
        public IActionResult CreateStudent([FromBody] StudentModel model)
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
                // Bắt các lỗi từ BLL hoặc lỗi trùng lặp dữ liệu từ SQL Server
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
        [HttpPut("update")]
        public IActionResult UpdateStudent([FromBody] StudentModel model)
        {
            try
            {
                _studentBusiness.UpdateStudent(model);
                return Ok(new { statusCode = 200, message = "Cập nhật thành công!" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpDelete("delete/{studentCode}")]
        public IActionResult DeleteStudent(string studentCode)
        {
            try
            {
                _studentBusiness.DeleteStudent(studentCode);
                return Ok(new { statusCode = 200, message = "Xóa thành công!" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("get-by-id/{studentCode}")]
        public IActionResult GetStudentById(string studentCode)
        {
            try
            {
                var data = _studentBusiness.GetStudentById(studentCode);
                if (data == null) return NotFound(new { message = "Không tìm thấy học viên!" });
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("search")]
        public IActionResult SearchStudents([FromQuery] string keyword = "")
        {
            try
            {
                var data = _studentBusiness.SearchStudents(keyword);
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}