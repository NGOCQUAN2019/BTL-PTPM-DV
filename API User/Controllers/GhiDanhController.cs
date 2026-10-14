using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL.Interfaces;

namespace APIUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "QuanTri,HocVien")]
    public class GhiDanhController : ControllerBase
    {
        private readonly IGhiDanhBusiness _ghiDanhBusiness;

        public GhiDanhController(IGhiDanhBusiness ghiDanhBusiness)
        {
            _ghiDanhBusiness = ghiDanhBusiness;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_ghiDanhBusiness.GetAll());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _ghiDanhBusiness.GetById(id);
            if (data == null) return NotFound(new { message = "Không tìm thấy hồ sơ ghi danh!" });
            return Ok(data);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] GhiDanhModel model)
        {
            if (_ghiDanhBusiness.Create(model))
                return Ok(new { message = "Ghi danh thành công!" });
            return BadRequest(new { message = "Ghi danh thất bại! Vui lòng kiểm tra lại MaHocVien hoặc LopHocId." });
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] GhiDanhModel model)
        {
            if (_ghiDanhBusiness.Update(model))
                return Ok(new { message = "Cập nhật thành công!" });
            return BadRequest(new { message = "Cập nhật thất bại!" });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_ghiDanhBusiness.Delete(id))
                return Ok(new { message = "Xóa thành công!" });
            return BadRequest(new { message = "Xóa thất bại!" });
        }
    }
}