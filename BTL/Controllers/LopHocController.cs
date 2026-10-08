using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL.Interfaces;

namespace API_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class LopHocController : ControllerBase
    {
        private readonly ILopHocBusiness _classBusiness;

        public LopHocController(ILopHocBusiness classBusiness)
        {
            _classBusiness = classBusiness;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_classBusiness.GetAll());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var classData = _classBusiness.GetById(id);
            if (classData == null) return NotFound(new { message = "Không tìm thấy lớp học" });
            return Ok(classData);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] LopHocModel model)
        {
            if (_classBusiness.Create(model))
                return Ok(new { message = "Thêm lớp học thành công!" });
            return BadRequest(new { message = "Thêm thất bại! Vui lòng kiểm tra lại CourseId và TeacherId có tồn tại hay không." });
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] LopHocModel model)
        {
            if (_classBusiness.Update(model))
                return Ok(new { message = "Cập nhật thành công!" });
            return BadRequest(new { message = "Cập nhật thất bại!" });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_classBusiness.Delete(id))
                return Ok(new { message = "Xóa lớp học thành công!" });
            return BadRequest(new { message = "Xóa thất bại! Lớp này có thể đang có học viên ghi danh." });
        }
    }
}