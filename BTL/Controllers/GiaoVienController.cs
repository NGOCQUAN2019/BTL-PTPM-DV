using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class GiaoVienController : ControllerBase
    {
        private readonly IGiaoVienBusiness _teacherBusiness;

        public GiaoVienController(IGiaoVienBusiness teacherBusiness)
        {
            _teacherBusiness = teacherBusiness;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_teacherBusiness.GetAll());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var teacher = _teacherBusiness.GetById(id);
            if (teacher == null) return NotFound(new { message = "Không tìm thấy giáo viên" });
            return Ok(teacher);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] GiaoVienModel model)
        {
            if (_teacherBusiness.Create(model))
                return Ok(new { message = "Thêm giáo viên thành công! Tài khoản hệ thống đã được tự động tạo." });
            return BadRequest(new { message = "Thêm thất bại!" });
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] GiaoVienModel model)
        {
            if (_teacherBusiness.Update(model))
                return Ok(new { message = "Cập nhật thành công!" });
            return BadRequest(new { message = "Cập nhật thất bại!" });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_teacherBusiness.Delete(id))
                return Ok(new { message = "Xóa thành công giáo viên và tài khoản liên quan!" });
            return BadRequest(new { message = "Xóa thất bại!" });
        }
    }
}