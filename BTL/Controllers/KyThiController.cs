using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Model;
using System;

namespace APIAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Admin,GiaoVien")] // Bỏ comment khi bạn muốn yêu cầu đăng nhập
    public class KyThiController : ControllerBase
    {
        private readonly IKyThiBusiness _kyThiBusiness;

        public KyThiController(IKyThiBusiness kyThiBusiness)
        {
            _kyThiBusiness = kyThiBusiness;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_kyThiBusiness.GetAll());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _kyThiBusiness.GetById(id);
            if (data == null)
                return NotFound(new { message = "Không tìm thấy kỳ thi này trong hệ thống!" });

            return Ok(data);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] KyThiModel model)
        {
            try
            {
                if (_kyThiBusiness.Create(model))
                    return Ok(new { message = "Thêm kỳ thi thành công!" });

                return BadRequest(new { message = "Thêm kỳ thi thất bại." });
            }
            catch (Exception ex)
            {
                // Bắt các bẫy lỗi từ tầng DAL ném lên
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] KyThiModel model)
        {
            try
            {
                if (_kyThiBusiness.Update(model))
                    return Ok(new { message = "Cập nhật kỳ thi thành công!" });

                return BadRequest(new { message = "Cập nhật kỳ thi thất bại." });
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
                if (_kyThiBusiness.Delete(id))
                    return Ok(new { message = "Xóa kỳ thi thành công!" });

                return BadRequest(new { message = "Xóa kỳ thi thất bại." });
            }
            catch (Exception ex)
            {
                // Bắt lỗi khóa ngoại nếu kỳ thi này đã có điểm số
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}