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
    }
}