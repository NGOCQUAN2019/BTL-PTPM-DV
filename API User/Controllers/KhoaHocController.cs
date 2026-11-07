using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL.Interfaces;

namespace API_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "QuanTri")] // Bắt buộc là Admin
    public class KhoaHocController : ControllerBase
    {
        private readonly IKhoaHocBusiness _khoaHocBusiness;

        public KhoaHocController(IKhoaHocBusiness khoaHocBusiness)
        {
            _khoaHocBusiness = khoaHocBusiness;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll() => Ok(_khoaHocBusiness.GetAll());

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id) => Ok(_khoaHocBusiness.GetById(id));

        [HttpPost("create")]
        public IActionResult Create([FromBody] KhoaHocModel model)
        {
            if (_khoaHocBusiness.Create(model)) return Ok(new { message = "Thêm khóa học thành công!" });
            return BadRequest(new { message = "Thêm khóa học thất bại!" });
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] KhoaHocModel model)
        {
            if (_khoaHocBusiness.Update(model)) return Ok(new { message = "Cập nhật khóa học thành công!" });
            return BadRequest(new { message = "Cập nhật thất bại!" });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_khoaHocBusiness.Delete(id)) return Ok(new { message = "Xóa khóa học thành công!" });
            return BadRequest(new { message = "Xóa thất bại!" });
        }
    }
}