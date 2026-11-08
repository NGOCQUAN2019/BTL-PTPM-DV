using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLL.Interfaces;

namespace API_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HocVien")]
    [AllowAnonymous]
    public class KhoaHocController : ControllerBase
    {
        private readonly IKhoaHocBusiness _khoaHocBusiness;

        public KhoaHocController(IKhoaHocBusiness khoaHocBusiness)
        {
            _khoaHocBusiness = khoaHocBusiness;
        }

        // Học viên xem toàn bộ danh sách khóa học đang có để chọn đăng ký
        [HttpGet("get-all")]
        public IActionResult GetAll() => Ok(_khoaHocBusiness.GetAll());

        // Học viên bấm vào xem chi tiết thông tin của 1 khóa học
        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id) => Ok(_khoaHocBusiness.GetById(id));
    }
}