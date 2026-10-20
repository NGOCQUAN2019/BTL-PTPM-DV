using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL.Interfaces;

namespace API_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "QuanTri")] // Chỉ Admin
    public class GhiDanhController : ControllerBase
    {
        private readonly IGhiDanhBusiness _ghiDanhBusiness;

        public GhiDanhController(IGhiDanhBusiness ghiDanhBusiness)
        {
            _ghiDanhBusiness = ghiDanhBusiness;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll() => Ok(_ghiDanhBusiness.GetAll());

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id) => Ok(_ghiDanhBusiness.GetById(id));

        [HttpPost("create")]
        public IActionResult Create([FromBody] GhiDanhModel model)
        {
            
            if (_ghiDanhBusiness.Create(model))
                return Ok(new { message = "Thêm ghi danh thành công!" });
            return BadRequest(new { message = "Thêm thất bại hoặc lớp đã đầy!" });
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] GhiDanhModel model)
        {
            if (_ghiDanhBusiness.Update(model))
                return Ok(new { message = "Cập nhật hồ sơ thành công!" });
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