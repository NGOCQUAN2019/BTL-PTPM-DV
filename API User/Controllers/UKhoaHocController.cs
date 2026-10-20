using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;

namespace APIUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HocVien")]
    public class UCourseController : ControllerBase
    {
        private readonly IKhoaHocBusiness _khoaHocBusiness;

        public UCourseController(IKhoaHocBusiness khoaHocBusiness)
        {
            _khoaHocBusiness = khoaHocBusiness;
        }

        // Học viên lấy danh sách toàn bộ khóa học để chọn lựa
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_khoaHocBusiness.GetAll());
        }

        // Học viên xem chi tiết 1 khóa học (để biết học phí, cấp độ, v.v.)
        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_khoaHocBusiness.GetById(id));
        }
    }
}