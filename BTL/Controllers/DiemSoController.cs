using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Model;
using System;

namespace APIAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiemSoController : ControllerBase
    {
        private readonly IDiemSoBusiness _diemSoBusiness;

        public DiemSoController(IDiemSoBusiness diemSoBusiness)
        {
            _diemSoBusiness = diemSoBusiness;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_diemSoBusiness.GetAll());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _diemSoBusiness.GetById(id);
            if (data == null)
                return NotFound(new { message = "Không tìm thấy dữ liệu điểm này!" });

            return Ok(data);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DiemSoModel model)
        {
            try
            {
                if (_diemSoBusiness.Create(model))
                    return Ok(new { message = "Nhập điểm thành công!" });

                return BadRequest(new { message = "Nhập điểm thất bại." });
            }
            catch (Exception ex)
            {
                // Hứng cái lỗi 2627 (Trùng lặp học viên trong cùng 1 kỳ thi)
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] DiemSoModel model)
        {
            try
            {
                if (_diemSoBusiness.Update(model))
                    return Ok(new { message = "Cập nhật điểm thành công!" });

                return BadRequest(new { message = "Cập nhật điểm thất bại." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_diemSoBusiness.Delete(id))
                    return Ok(new { message = "Xóa điểm thành công!" });

                return BadRequest(new { message = "Xóa điểm thất bại." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}