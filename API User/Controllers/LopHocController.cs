using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using System;

namespace API_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HocVien")] // Chốt chặn: Bắt buộc phải là Học viên đã đăng nhập
    public class LopHocController : ControllerBase
    {
        private readonly ILopHocBusiness _classBusiness;

        public LopHocController(ILopHocBusiness classBusiness)
        {
            _classBusiness = classBusiness;
        }

        // Học viên xem danh sách các lớp học
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_classBusiness.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Học viên xem chi tiết 1 lớp học
        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var classData = _classBusiness.GetById(id);
                if (classData == null) return NotFound(new { message = "Không tìm thấy lớp học này!" });

                return Ok(classData);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}