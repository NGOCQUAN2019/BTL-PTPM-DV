using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APIAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChungChiController : ControllerBase
    {
        private readonly IChungChiBusiness _chungChiBusiness;

        public ChungChiController(IChungChiBusiness chungChiBusiness)
        {
            _chungChiBusiness = chungChiBusiness;
        }

        [HttpGet("get-all")]
        [Authorize(Roles = "QuanTri,GiaoVien")] // Phân quyền cấp hàm: Cả Admin và Giáo viên đều xem được
        public IActionResult GetAll()
        {
            return Ok(_chungChiBusiness.GetAll());
        }

        [HttpPost("cap-chung-chi/{ghiDanhId}")]
        [Authorize(Roles = "QuanTri")] // Phân quyền cấp hàm: Chỉ Admin được cấp
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
        [Authorize(Roles = "QuanTri")] // Phân quyền cấp hàm: Chỉ Admin được xóa
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