using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Model;
using BLL.Interfaces;

namespace API_Admin.Controllers // Đổi namespace về project Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Chốt chặn bắt buộc phải đăng nhập
    public class KhoaHocController : ControllerBase
    {
        private readonly IKhoaHocBusiness _khoaHocBusiness;

        public KhoaHocController(IKhoaHocBusiness khoaHocBusiness)
        {
            _khoaHocBusiness = khoaHocBusiness;
        }

        // Cả Admin và Giáo viên đều được xem danh sách khóa học
        [HttpGet("get-all")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult GetAll() => Ok(_khoaHocBusiness.GetAll());

        [HttpGet("get-by-id/{id}")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult GetById(int id) => Ok(_khoaHocBusiness.GetById(id));

        // CHỈ ADMIN mới có quyền tạo mới, chỉnh sửa và xóa
        [HttpPost("create")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult Create([FromBody] KhoaHocModel model)
        {
            if (_khoaHocBusiness.Create(model)) return Ok(new { message = "Thêm khóa học thành công!" });
            return BadRequest(new { message = "Thêm khóa học thất bại!" });
        }

        [HttpPut("update")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult Update([FromBody] KhoaHocModel model)
        {
            if (_khoaHocBusiness.Update(model)) return Ok(new { message = "Cập nhật khóa học thành công!" });
            return BadRequest(new { message = "Cập nhật thất bại!" });
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult Delete(int id)
        {
            if (_khoaHocBusiness.Delete(id)) return Ok(new { message = "Xóa khóa học thành công!" });
            return BadRequest(new { message = "Xóa thất bại!" });
        }
    }
}