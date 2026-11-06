using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using System;

namespace APIAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChungChiController : ControllerBase
    {
        private readonly IChungChiBusiness _chungChiBusiness;

        public ChungChiController(IChungChiBusiness chungChiBusiness)
        {
            _chungChiBusiness = chungChiBusiness;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_chungChiBusiness.GetAll());
        }

        // Chỉ cần truyền GhiDanhId vào URL, không cần body JSON phức tạp
        [HttpPost("cap-chung-chi/{ghiDanhId}")]
        public IActionResult CapChungChi(int ghiDanhId)
        {
            try
            {
                if (_chungChiBusiness.CapChungChi(ghiDanhId))
                    return Ok(new { message = "Cấp chứng chỉ thành công!" });

                return BadRequest(new { message = "Cấp chứng chỉ thất bại." });
            }
            catch (Exception ex)
            {
                // Hứng thông báo lỗi nếu học viên nợ tiền, rớt môn hoặc đã có chứng chỉ
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_chungChiBusiness.Delete(id))
                    return Ok(new { message = "Thu hồi chứng chỉ thành công!" });

                return BadRequest(new { message = "Thu hồi chứng chỉ thất bại." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}