using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL.Interfaces;

namespace API_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")] // Chỉ Admin có Token hợp lệ mới được gọi API này
    public class CourseController : ControllerBase
    {
        private readonly ICourseBusiness _courseBusiness;

        public CourseController(ICourseBusiness courseBusiness)
        {
            _courseBusiness = courseBusiness;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_courseBusiness.GetAll());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var course = _courseBusiness.GetById(id);
            if (course == null) return NotFound(new { message = "Không tìm thấy khóa học" });
            return Ok(course);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CourseModel model)
        {
            if (_courseBusiness.Create(model))
                return Ok(new { message = "Thêm thành công!" });
            return BadRequest(new { message = "Thêm thất bại!" });
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] CourseModel model)
        {
            if (_courseBusiness.Update(model))
                return Ok(new { message = "Cập nhật thành công!" });
            return BadRequest(new { message = "Cập nhật thất bại!" });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_courseBusiness.Delete(id))
                return Ok(new { message = "Xóa thành công!" });
            return BadRequest(new { message = "Xóa thất bại!" });
        }
    }
}