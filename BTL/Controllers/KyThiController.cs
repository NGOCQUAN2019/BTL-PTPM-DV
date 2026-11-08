using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Model;
using System;

namespace API_Admin.Controllers // Đổi namespace về project Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Chốt chặn vòng ngoài: Bắt buộc đăng nhập
    public class KyThiController : ControllerBase
    {
        private readonly IKyThiBusiness _kyThiBusiness;

        public KyThiController(IKyThiBusiness kyThiBusiness)
        {
            _kyThiBusiness = kyThiBusiness;
        }

        // Cả Admin và Giáo viên đều được xem lịch/danh sách kỳ thi
        [HttpGet("get-all")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult GetAll()
        {
            return Ok(_kyThiBusiness.GetAll());
        }

        [HttpGet("get-by-id/{id}")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult GetById(int id)
        {
            var data = _kyThiBusiness.GetById(id);
            if (data == null)
                return NotFound(new { message = "Không tìm thấy kỳ thi này trong hệ thống!" });

            return Ok(data);
        }

        // ĐỘC QUYỀN ADMIN: Lên lịch thi, sửa lịch thi, xóa lịch thi
        [HttpPost("create")]
        [Authorize(Roles = "QuanTri")]
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
        [Authorize(Roles = "QuanTri")]
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
        [Authorize(Roles = "QuanTri")]
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