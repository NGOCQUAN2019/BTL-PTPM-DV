using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Model;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherBusiness _teacherBusiness;

        public TeacherController(ITeacherBusiness teacherBusiness)
        {
            _teacherBusiness = teacherBusiness;
        }

        [HttpPost("create")]
        public IActionResult CreateTeacher([FromBody] TeacherModel model)
        {
            try
            {
                _teacherBusiness.CreateTeacher(model);
                return Ok(new { statusCode = 200, message = "Thêm giáo viên thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("search")]
        public IActionResult SearchTeachers([FromQuery] string keyword = "")
        {
            try
            {
                var data = _teacherBusiness.SearchTeachers(keyword);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}